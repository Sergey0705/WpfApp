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
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
   [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
   internal class MainWindowViewModel : ViewModel
   {
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
       
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region FuelCount : double - Количество непонятно чего

        private string _FuelCount;

        public string FuelCount
        {
            get => _FuelCount;
            set => Set(ref _FuelCount, value);
        }

        #endregion


        #region Coefficient : double - Коэффициент

        private double _Coefficient = 1;

        public double Coefficient
        {
            get => _Coefficient;
            set => Set(ref _Coefficient, value);
        }

        #endregion



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



 

        #endregion

        /* ----------------------------------------------------- */

        public MainWindowViewModel(CountriesStatisticViewModel Statistic)
        {
            CountriesStatistic = Statistic;
            Statistic.MainModel = this;

            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            #endregion
        }
    }
}
