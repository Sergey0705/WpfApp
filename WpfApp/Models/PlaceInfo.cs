using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp.Models
{
    class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }
        public virtual IEnumerable<ConfirmedCount> Counts { get; set; }
        public override string ToString() => $"{Name}({Location})";
    }
}
