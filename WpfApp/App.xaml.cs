using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using WpfApp.Services;
using WpfApp.ViewModels;

namespace WpfApp
{
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost _Host;

        public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountriesStatisticViewModel>();
        }
    }
}
