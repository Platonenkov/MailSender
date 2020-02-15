using System;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(AdditionMulty))]
    public class AdditionMulty : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c) => 
            vv?.Length > 0 
                ? vv.Select(v => v is double value ? value : System.Convert.ToDouble((object) v)).Sum() 
                : double.NaN;
    }

    [MarkupExtensionReturnType(typeof(AdditionNegativeToNanMulty))]
    public class AdditionNegativeToNanMulty : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            if (vv?.Length > 0)
            {
                var rez = vv.Select(v => v is double value ? value : System.Convert.ToDouble((object)v)).Sum();
                return rez < 0 ? double.NaN : rez;
            }
            else
                return double.NaN;
        }
    }

}