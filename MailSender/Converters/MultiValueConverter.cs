using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MultiValueConverter))]
    public abstract class MultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider sp) => this;

        [CanBeNull] public abstract object Convert([CanBeNull, ItemCanBeNull] object[] vv, [NotNull] Type t, [CanBeNull] object p, [NotNull] CultureInfo c);

        [ItemCanBeNull, CanBeNull] public virtual object[] ConvertBack([CanBeNull] object v, [ItemNotNull, NotNull] Type[] tt, [CanBeNull] object p, [NotNull] CultureInfo c) => throw new NotSupportedException();
    }
}