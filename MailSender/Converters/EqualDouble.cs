using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    /// <summary>
    /// сравнивает два double значения на равенство, IsNaN(value) сравниваемое с  0 - вернёт true
    /// </summary>
    [ValueConversion(typeof(double), typeof(bool)), MarkupExtensionReturnType(typeof(EqualDouble))]
    public class EqualDouble : ValueConverter
    {
        /// <summary>
        /// значение с которым сравниваем, IsNaN(value) сравниваемое с  0 - вернёт true
        /// </summary>
        public double Value { get; set; }

        public EqualDouble() { }

        public EqualDouble(double value) => Value = value;

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = DoubleValueConverter.ToDouble(v);

            return double.IsNaN(value) && Value == 0 ? true : double.IsNaN(value) ? (object)false : value == Value;
        }
    }
}