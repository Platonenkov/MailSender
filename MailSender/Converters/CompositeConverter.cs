using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [ContentProperty("Converters"), MarkupExtensionReturnType(typeof(CompositeConverter))]
    public class CompositeConverter : ValueConverter
    {
        [NotNull] private readonly IList<IValueConverter> _Converters = new List<IValueConverter>();

        /// <summary>Коллекция конвертеров</summary>
        [NotNull]
        public ICollection<IValueConverter> Converters => _Converters;

        public CompositeConverter() { }

        public CompositeConverter([NotNull] IValueConverter converter) => _Converters.Add(converter);

        public CompositeConverter([NotNull] IValueConverter c1, [NotNull] IValueConverter c2) : this(c1) => _Converters.Add(c2);
        public CompositeConverter([NotNull] IValueConverter c1, [NotNull] IValueConverter c2, [NotNull] IValueConverter c3) : this(c1, c2) => _Converters.Add(c3);
        public CompositeConverter([NotNull] IValueConverter c1, [NotNull] IValueConverter c2, [NotNull] IValueConverter c3, [NotNull] IValueConverter c4) : this(c1, c2, c3) => _Converters.Add(c4);

        public CompositeConverter([ItemNotNull] [NotNull] params IValueConverter[] converters) => _Converters = new List<IValueConverter>(converters);

        #region IValueConverter

        public override object Convert(object value, Type t, object p, CultureInfo culture) => Converters.Aggregate(value, (v, c) => c.Convert(v, t, p, culture));

        public override object ConvertBack(object value, Type t, object p, CultureInfo culture) => Converters.Reverse().Aggregate(value, (v, c) => c.ConvertBack(v, t, p, culture));

        #endregion

        [ContractInvariantMethod]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Требуется для CodeContracts")]
        private void ObjectInvariant() => Contract.Invariant(_Converters != null);
    }
}