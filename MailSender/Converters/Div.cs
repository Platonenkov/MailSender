using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    /// <summary>Конвертер целочисленного деления для целых чисел</summary>
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Div))]
    public class Div : ParameterMathConverter
    {
        public Div() : this(1) { }
        public Div(int m) : base(m, (v, M) => (int)v / M, (v,M) => (int)v * M) { }
    }
}