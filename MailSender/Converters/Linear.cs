using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    /// <summary>Линейный конвертер вещественных величин по формуле result = K*v + B</summary>
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(Linear))]
    public class Linear : DoubleValueConverter
    {
        /// <summary>Линейная интерполяция значения между двумя точками</summary>
        /// <param name="x1">Начало интервала интерполяции</param>
        /// <param name="y1">Значение на начале интервала интерполяции</param>
        /// <param name="x2">Конец интервала интерполяции</param>
        /// <param name="y2">Значение на конце интервала интерполяции</param>
        /// <param name="x">Текущее значение на интервале интерполяции</param>
        /// <returns>Значение на текущем значении в интервале интерполяции</returns>
        public static double Interpolate(double x1, double y1, double x2, double y2, double x) => (x - x1) * (y2 - y1) / (x2 - x1) + y1;

        /// <summary>Линейный множитель (тангенс угла наклона)</summary>
        public double K { get; set; }
        /// <summary>Аддитивное смещение</summary>
        public double B { get; set; }

        /// <summary>Линейный интерполятор</summary>
        public Linear() : this(1) { }

        /// <summary>Линейный интерполятор</summary>
        /// <param name="k">Линейный множитель (тангенс угла наклона)</param>
        public Linear(double k) => K = k;

        protected override double To(double v, object p) => v * K + B;

        protected override double From(double v, object p) => (v - B) / K;
    }
}