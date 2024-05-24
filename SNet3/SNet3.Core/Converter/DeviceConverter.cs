using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SNet3.Core.Converter
{
    public class VoltageToStringConveter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var voltage = value as double?;
            if (voltage.HasValue)
                return $"{Math.Round(voltage.Value, 3)}V";
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    
    public class CurrentToStringConveter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var current = value as double?;
            if (current.HasValue)
                return $"{Math.Round(current.Value, 3)}A";
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class TemperatureToStringConveter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temperature = value as double?;
            if (temperature.HasValue)
                return $"{Math.Round(temperature.Value, 1)}℃";
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class SocketDataConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is byte[] bytes))
                return null;

            var stringBuilder = new StringBuilder();
            return $"[{DateTime.Now.ToString("HH:mm:ss")}] - {BitConverter.ToString(bytes)}";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
