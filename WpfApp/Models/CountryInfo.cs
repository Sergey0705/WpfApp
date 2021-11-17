using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
}
