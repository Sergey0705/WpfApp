using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.Infrastructure.Converters
{
    internal class ParametricMultiplyValueConverter : Freezable, IValueConverter
    {
        #region Angle : double - Прибавляемое значение

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricMultiplyValueConverter),
                new PropertyMetadata(1.0));

        //[Category("Моя категория!")]
        [Description("Прибавляемое значение")]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion
        public object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);

            return value * Value;
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);

            return value / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultiplyValueConverter { Value = Value };


     
       
    }
}
