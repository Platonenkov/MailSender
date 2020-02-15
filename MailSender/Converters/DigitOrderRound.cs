using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(DigitOrderRound))]
    public class DigitOrderRound : DoubleValueConverter
    {
        /// <summary>Делитель (100 снимает 2 порядка)</summary>
        public double K { get; set; }

        /// <summary>Округление до сотен и т.д.</summary>
        public DigitOrderRound() : this(1) { }

        /// <summary>Округление до сотен и т.д.</summary>
        /// <param name="k">Делитель</param>
        public DigitOrderRound(double k) => K = k;

        protected override double To(double v, object p)
        {
            var result = ((int)Math.Round(v / K)) * K;
            return result;
        } 
    }
}
