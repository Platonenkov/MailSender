using System;
using System.Globalization;
using MathCore.Annotations;

namespace MailSender.Converters
{
    public class ConverterEventArgs : EventArgs
    {
        [CanBeNull] public object ConvertedValue { get; set; }
        [CanBeNull] public object Value { get; private set; }
        [NotNull] public Type TargetType { get; private set; }
        [CanBeNull] public object Parameter { get; private set; }
        [NotNull] public CultureInfo Culture { get; private set; }

        public ConverterEventArgs([CanBeNull] object value, [NotNull] Type targetType, [CanBeNull] object parameter, [NotNull] CultureInfo culture)
        {
            TargetType = targetType;
            Parameter = parameter;
            Culture = culture;
            Value = value;
        }
    }
}