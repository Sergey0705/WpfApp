using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp.Models
{
    class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }
        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
