using System;
using System.Globalization;
using System.Windows.Data;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Converters.Base
{
    internal abstract class ConverterBase : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException("Обратное преобразование не поддерживается");

    }
}
