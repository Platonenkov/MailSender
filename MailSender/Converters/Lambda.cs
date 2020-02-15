using System;
using System.Globalization;
using MathCore.Annotations;

namespace MailSender.Converters
{
    public class Lambda<T, Q> : ValueConverter
    {
        [NotNull] private readonly Func<T, Type, object, CultureInfo, Q> _Converter;
        [NotNull] private readonly Func<Q, Type, object, CultureInfo, T> _BackConverter;

        public Lambda([NotNull]Func<T, Q> Converter, [CanBeNull] Func<Q, T> BackConverter = null)
            : this((v, t, p, c) => Converter(v), BackConverter is null ? null : (Func<Q, Type, object, CultureInfo, T>)((v, t, p, c) => BackConverter(v)))
        { }

        public Lambda([NotNull]Func<T, Type, object, CultureInfo, Q> Converter, [CanBeNull] Func<Q, Type, object, CultureInfo, T> BackConverter = null)
        {
            _Converter = Converter;
            _BackConverter = BackConverter ?? ((q, t, p, c) => throw new NotSupportedException());
        }

        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) => _Converter((T)v, t, p, c);

        /// <inheritdoc />
        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => _BackConverter((Q)v, t, p, c);
    }
}