using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(MultiBoolVisibilityValueConverter))]
    public class MultiBoolVisibilityValueConverter : IMultiValueConverter
    {
        [CanBeNull]
        public object Convert([CanBeNull, ItemCanBeNull] object[] vv, [NotNull] Type t, [CanBeNull] object p,
            [NotNull] CultureInfo c)
        {
            return vv.IsContains(true) ? true : (object)false;
        }

        [ItemCanBeNull, CanBeNull] public object[] ConvertBack([CanBeNull] object v, [ItemNotNull, NotNull] Type[] tt, [CanBeNull] object p, [NotNull] CultureInfo c) => throw new NotSupportedException();
    }
}