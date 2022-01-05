using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Models.Decanat;
using WpfApp.Services.Interfaces;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
   [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
   internal class MainWindowViewModel : ViewModel
   {
        private readonly IAsyncDataService _AsyncData;

        /* ----------------------------------------------------- */

        public CountriesStatisticViewModel CountriesStatistic { get; }

        /* ----------------------------------------------------- */

        #region SelectedFilteredText : string - Текст фильтра студентов

        private string _StudentFilterText;

        public string StudentFilterText 
        {
            get => _StudentFilterText;
            set 
            {
                if(!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            } 
        }

        #endregion

        #region SelectedGroupStudents

        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student)) return;

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if (student.Name is null || student.Surname is null || student.Patronymic is null) return;

            if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        #endregion

        #region SelectedPageIndex

        private int _selectedPageIndex = 1;

        public int SelectedPageIndex
        {
            get => _selectedPageIndex;
            set => Set(ref _selectedPageIndex, value);
        }

        #endregion

        #region DataValue : string - Результат длительной асинхронной операции

        private string _DataValue;

        public string DataValue
        {
            get => _DataValue;
            private set => Set(ref _DataValue, value);
        }

        #endregion


        #region TestDataPoints

        private IEnumerable<DataPoint> _testDataPoints;

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }


        #endregion

        #region Заголовок окна
        private string _title = "Анализ статистики CV19";
       
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _title;
            //set
            //{
            //    if (Equals(_title, value)) return;
            //    _title = value;
            //    OnPropertyChanged();

            //    Set(ref _title, value)
            //}
            set => Set(ref _title, value);
        }
        #endregion

        #region Status : string - Статус программы

        private string _status = "Готов!";
        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        public IEnumerable<Student> TestStudents => 
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(i => new Student
            {
                Name = $"Имя {i}",
                Surname = $"Фамилия {i}"
            });

   
      

        /* ----------------------------------------------------- */

        #region Команды

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _selectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }


        #endregion

        #region ClosedApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            (RootObject as Window)?.Close();
            //Application.Current.Shutdown();
        }

        #endregion

        #region Command StartProcessCommand - Запуск процесса

        public ICommand StartProcessCommand { get; }

        private static bool CanStartProcessCommandExecuted(object p) => true;

        private void OnStartProcessCommandExecuted(object p)
        {
            DataValue = _AsyncData.GetResult(DateTime.Now);
        }

        #endregion

        #region Command StopProcessCommand - Остановка процесса

        public ICommand StopProcessCommand { get; }

        private static bool CanStopProcessCommandExecuted(object p) => true;

        private void OnStopProcessCommandExecuted(object p)
        {

        }

        #endregion

        #endregion

        /* ----------------------------------------------------- */

        public MainWindowViewModel(CountriesStatisticViewModel Statistic, IAsyncDataService AsyncData)
        {
            _AsyncData = AsyncData;
            CountriesStatistic = Statistic;
            Statistic.MainModel = this;

            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            StartProcessCommand = new LambdaCommand(OnStartProcessCommandExecuted, CanStartProcessCommandExecuted);
            StopProcessCommand = new LambdaCommand(OnStopProcessCommandExecuted, CanStopProcessCommandExecuted);

            #endregion
        }
    }
}
