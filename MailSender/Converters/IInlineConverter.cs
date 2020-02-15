using System;
using System.Windows.Data;

namespace MailSender.Converters
{
    public interface IInlineConverter : IValueConverter
    {
        EventHandler<ConverterEventArgs> Converting { get; set; }
        EventHandler<ConverterEventArgs> ConvertingBack { get; set; }
    }
}