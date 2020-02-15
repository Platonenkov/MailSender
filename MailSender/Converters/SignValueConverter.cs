using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(SignValueConverter))]
    public class SignValueConverter : DoubleValueConverter
    {
        public double Delta { get; set; }

        public bool Inverse { get; set; }

        public SignValueConverter() { }
        public SignValueConverter(double Delta) => this.Delta = Delta;
        public SignValueConverter(bool Inverse) => this.Inverse = Inverse;

        protected override double To(double v, object p) =>
            double.IsNaN(v) 
                ? v 
                : Math.Abs(v) <= Delta 
                    ? 0d 
                    : Inverse 
                        ? -Math.Sign(v) 
                        : Math.Sign(v);
    }
}