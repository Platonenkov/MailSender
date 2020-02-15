using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Values2Point))]
    public class Mod : ParameterMathConverter
    {
        public Mod() : this(double.MaxValue) { }
        public Mod(double mod) : base(mod, (v, M) => v % M) { }
    }
}