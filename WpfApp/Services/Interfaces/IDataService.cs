using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Models;

namespace WpfApp.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
