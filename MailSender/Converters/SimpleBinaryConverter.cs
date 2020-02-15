using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(SimpleBinaryConverter))]
    public abstract class SimpleBinaryConverter : MathConverter
    {
        protected double _Value;

        public double Value { get => _Value; set => _Value = value; }

        protected SimpleBinaryConverter(double value) => _Value = value;

        protected abstract double To(double v);

        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => To((double)System.Convert.ChangeType(v, typeof(double)));

        protected virtual double From(double v) => throw new NotSupportedException("Обратное преобразование не поддерживается");

        /// <inheritdoc />
        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => From((double)System.Convert.ChangeType(v, typeof(double)));
    }
}