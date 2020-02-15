using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Double_Precise1_Converter))]
    public class Double_Precise1_Converter : DoubleValueConverter
    {
        protected override double To(double v, object p) => Math.Round((double)v, 1);
        protected override double From(double v, object p) => Math.Round(v, 1);
    }
}
