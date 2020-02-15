using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(string), typeof(string)), MarkupExtensionReturnType(typeof(SubString))]
    public class SubString : ValueConverter
    {
        public int StartIndex { get; set; }

        public int Length { get; set; } = -1;

        public SubString() { }

        public SubString(int StartIndex, int Length = -1)
        {
            this.StartIndex = StartIndex;
            this.Length = Length;
        }

        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c) =>
            v is string str
                ? Length == 0
                    ? ""
                    : Length < 0
                        ? str.Substring(StartIndex)
                        : str.Substring(StartIndex, Length)
                : null;
    }
}
