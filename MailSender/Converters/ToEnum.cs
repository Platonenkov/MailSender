using System;
using System.Globalization;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(ToEnum))]
    public class ToEnum : ValueConverter
    {
        [CanBeNull] private Type _TargetType;

        [CanBeNull]
        public Type TargetType
        {
            get => _TargetType;
            set
            {
                if (value != null && !value.IsEnum) throw new ArgumentException("Тип не является перечислением", nameof(value));
                _TargetType = value;
            }
        }

        public ToEnum() { }
        public ToEnum([NotNull] Type Target) => TargetType = Target;

        public override object Convert(object v, Type t, object p, CultureInfo c) => v is null ? null : Enum.ToObject(p as Type ?? TargetType ?? t, v);

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v is null ? null : System.Convert.ChangeType(v, t);
    }
}