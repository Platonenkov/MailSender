using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(double), typeof(TimeSpan)), MarkupExtensionReturnType(typeof(SecondsToTimeSpan))]
    public class SecondsToTimeSpan : ValueConverter
    {
        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            var time = System.Convert.ToDouble(v);
            switch (p)
            {
                case double seconds_offset:
                    time += seconds_offset;
                    break;
                case TimeSpan time_offset:
                    time += time_offset.Seconds;
                    break;
                case string time_string_offset:
                    if (double.TryParse(time_string_offset, out var time_string_seconds))
                        time += time_string_seconds;
                    else if (TimeSpan.TryParse(time_string_offset, out var time_string_offset_timespan))
                        time += time_string_offset_timespan.Seconds;
                    else throw new FormatException("Неверный формат смещения времени", new ArgumentException(nameof(p)));
                    break;
            }
            if (double.IsNaN(time))
                return null;

            return TimeSpan.FromSeconds(time);
        }

        /// <inheritdoc />
        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => v is TimeSpan time ? time.TotalSeconds : (object)null;
    }
}
