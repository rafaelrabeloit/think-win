using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Think.WinStore
{
    class ConverterDisplaySize : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string str)
        {
            var bounds = Window.Current.Bounds;

            double height = bounds.Height;
            double width = bounds.Width;
		    
	        //TODO: Usar o parameter e colocar tambem no width do item no snaped mode
            return width - 150;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string str)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}
