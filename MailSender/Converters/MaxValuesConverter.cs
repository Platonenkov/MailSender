using System;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MaxValuesConverter))]
    public class MaxValuesConverter : MultiValueConverter
    {                                                                                     
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => vv?.Max();
    }
}