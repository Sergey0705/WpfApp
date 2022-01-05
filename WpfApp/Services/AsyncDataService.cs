using System;
using System.Threading;
using WpfApp.Services.Interfaces;

namespace WpfApp.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int _SleepTime = 7000;
        public string GetResult(DateTime Time)
        {
            Thread.Sleep(_SleepTime);

            return $"Result Value {Time}";
        }
    }
}
