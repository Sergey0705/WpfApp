using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.ViewModels
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<CountriesStatisticViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<WebServerViewModel>();

            return services;
        }
    }
}
