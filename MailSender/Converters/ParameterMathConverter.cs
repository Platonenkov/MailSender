using System;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(double)), MarkupExtensionReturnType(typeof(ParameterMathConverter))]
    public abstract class ParameterMathConverter : DoubleValueConverter
    {
        [NotNull] private readonly Func<double, double, double> _To;
        [CanBeNull] private readonly Func<double, double, double> _From;

        public double Parameter { get; set; }

        protected ParameterMathConverter(double Parameter, [NotNull] Func<double, double, double> to, [CanBeNull] Func<double, double, double> from = null)
        {
            this.Parameter = Parameter;
            _To = to;
            _From = from;
        }

        protected override double To(double v, object p) => _To(v, Parameter);

        protected override double From(double v, object p) => (_From ?? throw new NotSupportedException()).Invoke(v, Parameter);
    }
}