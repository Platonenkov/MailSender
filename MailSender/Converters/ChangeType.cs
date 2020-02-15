using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{

    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    [MarkupExtensionReturnType(typeof(ChangeType))]
    public class ChangeType : ValueConverter
    {
        [CanBeNull] public Type TargetType { get; set; }

        public ChangeType() { }
        public ChangeType([CanBeNull] Type Target) => TargetType = Target;

        public override object Convert(object v, Type t, object p, CultureInfo c) => v is null ? null : System.Convert.ChangeType(v, p as Type ?? TargetType ?? t, c);

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v is null ? null : System.Convert.ChangeType(v, t, c);
    }
}
