using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Services.Interfaces;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private IDataService _DataService;
        public MainWindowViewModel MainModel { get; internal set; }

        #region Countries : IEnumerable<CountryInfo> - Статистика по странам

        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value); 
        }

        #endregion

        #region SelectedCountry : CountryInfo - Выбранная страна

        private CountryInfo _SelectedCountry;

        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            private set => Set(ref _SelectedCountry, value);
        }

        #endregion

        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }

        #endregion

        /// <summary>
        /// Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере
        /// </summary>
        //public CountriesStatisticViewModel() : this(null)
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException("Вызов конструктора, непредназначенного для использования в обычном режиме");

        //    _Countries = Enumerable.Range(1, 10)
        //        .Select(i => new CountryInfo
        //        {
        //            Name = $"Country {i}",
        //            Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo
        //            {
        //                Name = $"Province {i}",
        //                Location = new Point(i, j),
        //                Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
        //                {
        //                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
        //                    Count = k
        //                }).ToArray()
        //            }).ToArray()
        //        }).ToArray();
        //}

        public CountriesStatisticViewModel(IDataService DataService)
        {

            _DataService = DataService;

            #region Команды

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}
