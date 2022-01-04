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



        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountriesStatisticViewModel>();
        }
    }
}
