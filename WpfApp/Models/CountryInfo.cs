using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}
