using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Services;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModel { get; }

        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;

            _DataService = new DataService();
        }
    }
}
