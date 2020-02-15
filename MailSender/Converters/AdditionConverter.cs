using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(AdditionConverter))]
    public class AdditionConverter : ParameterMathConverter
    {
        public AdditionConverter() : this(0) { }

        public AdditionConverter(double value) : base(value, (v,p) => v + p, (v,p) => v - p) { }
    }
}