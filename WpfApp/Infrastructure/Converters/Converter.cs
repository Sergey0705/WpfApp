﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp.Infrastructure.Converters
{
    internal abstract class Converter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;
   
        public abstract object Convert(object value, Type t, object p, CultureInfo c);

        public virtual object ConvertBack(object value, Type t, object p, CultureInfo c)
        {
            throw new NotSupportedException("Обратное преобразование не поддерживается");
        }
    }
}
