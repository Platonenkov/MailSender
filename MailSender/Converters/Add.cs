using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Add))]
    public class Add : ParameterMathConverter
    {
        public Add() : this(0) { }
        public Add(double p) : base(p, (v, x) => v + x, (r, x) => r - x) { }
    }
}