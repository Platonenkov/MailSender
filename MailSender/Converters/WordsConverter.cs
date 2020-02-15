using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSender.Converters
{
    [ValueConversion(typeof(int[]), typeof(string)), MarkupExtensionReturnType(typeof(WordsConverter))]
    public class WordsConverter : ValueConverter
    {
        public override object Convert(object v, Type t, object p, CultureInfo c) => 
            v is int[] words 
                ? string.Join(",", words.Select(w => w + 1)) 
                : string.Empty;

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is string str)) return null;
            var parts = str.Split(',');
            var words = new int[parts.Length];
            for (var i = 0; i < parts.Length; i++)
                if (int.TryParse(parts[i].Trim(), out var result))
                    words[i] = result - 1;

            return words;
        }
    }
}
