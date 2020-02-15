using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string)), MarkupExtensionReturnType(typeof(DateToStringConverter))]
    public class DateToStringConverter : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => 
            v is null ? "" :
                v is DateTime time ? time.ToShortDateString() :
                throw new Exception("Данный конвертер возможно использовать только с типом DateTime");

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => 
            DateTime.TryParse(System.Convert.ToString(v), c, DateTimeStyles.None, out var result) ? result : DateTime.MinValue;
    }
}
