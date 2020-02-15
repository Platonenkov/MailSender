using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;

// ReSharper disable UnusedMember.Global

namespace MailSender.Converters
{
    [ValueConversion(typeof(Type), typeof(Assembly)), MarkupExtensionReturnType(typeof(GetTypeAssembly))]
    public class GetTypeAssembly : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => ((Type)v).Assembly;
    }
}