using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    /// <summary>
    /// все true
    /// </summary>
    [MarkupExtensionReturnType(typeof(ContainsAllTrue))]
    public class ContainsAllTrue : IMultiValueConverter
    {
        [CanBeNull]
        public object Convert([CanBeNull, ItemCanBeNull] object[] vv, [NotNull] Type t, [CanBeNull] object p,
            [NotNull] CultureInfo c) =>!vv.IsContains(false) ;

        [ItemCanBeNull, CanBeNull] public object[] ConvertBack([CanBeNull] object v, [ItemNotNull, NotNull] Type[] tt, [CanBeNull] object p, [NotNull] CultureInfo c) => throw new NotSupportedException();
    }
}