using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(Visibility)), MarkupExtensionReturnType(typeof(NaNtoVisibility))]
    // ReSharper disable once InconsistentNaming
    public class NaNtoVisibility : ValueConverter
    {
        public bool Colapse { get; set; }

        public bool Inverted { get; set; }

        public override object Convert(object v, Type t, object p, CultureInfo c) => 
            Inverted 
                ? !double.IsNaN((double) v) 
                    ? (Colapse 
                        ? Visibility.Collapsed 
                        : Visibility.Hidden) 
                    : Visibility.Visible 
                : double.IsNaN((double) v) 
                    ? (Colapse 
                        ? Visibility.Collapsed 
                        : Visibility.Hidden) 
                    : Visibility.Visible;
    }
    [ValueConversion(typeof(object), typeof(Visibility)), MarkupExtensionReturnType(typeof(NullToVisibility))]
    public class NullToVisibility : ValueConverter
    {
        public bool Colapse { get; set; }

        public bool Inverted { get; set; }

        public override object Convert(object v, Type t, object p, CultureInfo c) => 
            Inverted 
                ? v.IsNotNull() 
                    ? (Colapse 
                        ? Visibility.Collapsed 
                        : Visibility.Hidden) 
                    : Visibility.Visible 
                : v.IsNull() 
                    ? (Colapse 
                        ? Visibility.Collapsed 
                        : Visibility.Hidden) 
                    : Visibility.Visible;
    }
}