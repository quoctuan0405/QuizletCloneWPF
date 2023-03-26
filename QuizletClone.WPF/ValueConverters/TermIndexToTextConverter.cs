using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace QuizletClone.WPF.ValueConverters
{
    public class TermIndexToTextConverter : MarkupExtension, IValueConverter
    {
        private static TermIndexToTextConverter mConverter = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int termIndex = (int)value;

            return "Term " + (termIndex + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new TermIndexToTextConverter());
        }
    }
}
