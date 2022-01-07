using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Services.Interfaces;
using WpfApp.ViewModels.Base;

namespace WpfApp.ViewModels
{
    internal class WebServerViewModel : ViewModel
    {
        private readonly IWebServerService _Server;

        #region Enabled

        private bool _Enabled;

        public bool Enabled { get => _Enabled; set => Set(ref _Enabled, value); }

        #endregion

        #region StartCommand

        private ICommand _StartCommand;

        public ICommand StartCommand => _StartCommand
            ?? new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !_Enabled;

        private void OnStartCommandExecuted(object p)
        {
            Enabled = true;
        }

        #endregion

        #region StopCommand

        private ICommand _StopCommand;

        public ICommand StopCommand => _StopCommand
            ?? new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        public IWebServerService Server { get; }

        private bool CanStopCommandExecute(object p) => _Enabled;

        private void OnStopCommandExecuted(object p)
        {
            Enabled = false;
        }

        #endregion

        public WebServerViewModel(IWebServerService Server) => _Server = Server;



    }
}
