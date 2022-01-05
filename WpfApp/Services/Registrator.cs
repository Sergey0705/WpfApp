using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Services.Interfaces;

namespace WpfApp.Services
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();

            services.AddTransient<IAsyncDataService, AsyncDataService>();

            return services;
        }
    }
}
