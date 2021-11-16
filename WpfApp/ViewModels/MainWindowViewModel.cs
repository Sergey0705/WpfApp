using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
   internal class MainWindowViewModel : ViewModel
   {
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
    }
}
