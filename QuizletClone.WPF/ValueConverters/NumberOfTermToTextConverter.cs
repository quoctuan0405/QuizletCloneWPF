using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace QuizletClone.WPF.ValueConverters
{
    public class NumberOfTermToTextConverter : MarkupExtension, IValueConverter
    {
        private static NumberOfTermToTextConverter mConverter = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numberOfTerm = (int)value;

            return numberOfTerm.ToString() + " terms";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new NumberOfTermToTextConverter());
        }
    }
}
