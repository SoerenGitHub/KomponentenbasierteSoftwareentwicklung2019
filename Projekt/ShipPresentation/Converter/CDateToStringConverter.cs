using FlightLogic.Utils;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
namespace SeaSkyPresentation.Converter
{

    public class CDateToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                char[] aschararray = value.ToString().ToCharArray();
                aschararray[2] = '/';
                aschararray[5] = '/';
                string svalue = new string(aschararray).Substring(0, 10);
                return DateTime.ParseExact(svalue, "dd/MM/yyyy", null);
            }
            return new DateTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.ToString());
            if (value != null)
            {
                string str = value.ToString();
                if (str == null) return "";
                FLog.FD("CDateToStringConverter.Convert()", " DateString: " + str);
                return str.Substring(0, 10);
            }
            return "";
        }
    }
}