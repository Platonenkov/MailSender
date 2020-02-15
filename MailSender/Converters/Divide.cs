using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    /// <summary>Конвертер вещественного деления на константу</summary>
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Values2Point))]
    public class Divide : ParameterMathConverter
    {
        /// <summary>Инициализация нового конвертера деления вещественного числа на константу</summary>
        /// <param name="K">Вещественный делитель</param>
        public Divide() : this(1) { }
        public Divide(double K) : base(K, (v, x) => v / x, (r, x) => r * x) { }
    }
}