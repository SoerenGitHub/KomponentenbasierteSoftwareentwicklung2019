using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;
namespace SeaSkyPresentation.Converter
{

    public class CStringToIntConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.ToString());
            string sValue = value.ToString();
            int num;
            if (!int.TryParse(sValue, out num)) num = 0;
            return num;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}