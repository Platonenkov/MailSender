using System;
using System.Globalization;
using MathCore.Annotations;

namespace MailSender.Converters
{
    public abstract class DoubleValueConverter : ValueConverter
    {
        public static double ToDouble([CanBeNull] object v) => v  is null ? double.NaN : v is double d ? d : System.Convert.ToDouble(v);
        public static double ToDouble([CanBeNull] object v, [NotNull] IFormatProvider format) => 
            v  is null ? double.NaN : v is double d ? d : System.Convert.ToDouble(v, format);

        public bool PassNaN { get; set; }

        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = ToDouble(v, c);
            if (double.IsNaN(value) && !PassNaN) return value;
            return To(value, p);
        }

        /// <inheritdoc />
        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var value = ToDouble(v, c);
            if (double.IsNaN(value) && !PassNaN) return value;
            return From(value, p);
        }

        protected abstract double To(double v, [CanBeNull] object p);
        protected virtual double From(double v, [CanBeNull] object p) => throw new NotSupportedException();
    }
}