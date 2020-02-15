using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MailSender.Converters
{

    [ValueConversion(typeof(Dictionary<double, string>), typeof(string))]
    public class TextCodedValuesConverter : ValueConverter
    {
        public override object Convert(object value, Type t, object p, CultureInfo c) =>
            value is null ? "" : ((Dictionary<double, string>)value).Aggregate
            (
                new StringBuilder(),
                (S, v) => S.AppendFormat("{0}={1},", v.Key, v.Value),
                S => S.ToString()
            ).Trim(',');

        public override object ConvertBack(object value, Type t, object p, CultureInfo c) =>
            ((string)value)?.Split(',')
               .Select(v => v.Split('='))
               .Where(v => v.Length >= 2)
               .Select(v => new KeyValuePair<double, string>(double.TryParse(v[0], out var V) ? V : double.NaN, v[1]))
               .Where(v => !double.IsNaN(v.Key))
               .ToDictionary(v => v.Key, v => v.Value);
    }
}
