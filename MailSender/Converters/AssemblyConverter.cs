using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [ValueConversion(typeof(Assembly), typeof(object))]
    public abstract class AssemblyConverter : ValueConverter
    {
        [NotNull]
        protected static Func<Assembly, object> GetAttributeValue<T>([NotNull] Func<T, object> Converter)
            where T : Attribute => asm =>
        {
            var a = asm.GetCustomAttributes(typeof(T), false).OfType<T>().FirstOrDefault();
            return a is null ? null : Converter(a);
        };

        [NotNull] private readonly Func<Assembly, object> _Converter;

        protected AssemblyConverter([NotNull] Func<Assembly, object> Converter) => _Converter = Converter;

        public override object Convert(object v, Type t, object p, CultureInfo c) => _Converter((Assembly)v);
    }
}