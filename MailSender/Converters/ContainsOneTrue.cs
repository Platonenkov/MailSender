using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    /// <summary>
    /// содержит true
    /// </summary>
    [MarkupExtensionReturnType(typeof(ContainsOneTrue))]
    public class ContainsOneTrue : IMultiValueConverter
    {
        [CanBeNull]
        public object Convert([CanBeNull, ItemCanBeNull] object[] vv, [NotNull] Type t, [CanBeNull] object p,
            [NotNull] CultureInfo c) =>
            vv.IsContains(true) ? true : (object)false;

        [ItemCanBeNull, CanBeNull] public object[] ConvertBack([CanBeNull] object v, [ItemNotNull, NotNull] Type[] tt, [CanBeNull] object p, [NotNull] CultureInfo c) => throw new NotSupportedException();
    }
}