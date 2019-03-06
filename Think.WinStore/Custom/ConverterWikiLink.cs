using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Think.WinStore
{
    class ConverterWikiLink : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string str)
        {
            return "http://www.wikipedia.org/wiki/Special:Search/" + (string)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string str)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
