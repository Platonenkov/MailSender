using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [ValueConversion(typeof(string), typeof(string)), MarkupExtensionReturnType(typeof(Regexp))]
    public class Regexp : ValueConverter
    {
        [CanBeNull] private Regex _Regexp;
        [CanBeNull] private string _Pattern;

        [CanBeNull]
        public string Pattern
        {
            get => _Pattern;
            set => _Regexp = new Regex(_Pattern = value ?? throw new ArgumentNullException(nameof(value)));
        }

        [CanBeNull] public string ReplacePattern { get; set; }

        public Regexp() { }

        public Regexp([NotNull] string Pattern) => this.Pattern = Pattern;

        /// <inheritdoc />
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (v is null) return null;
            if (!(v is string str)) str = v.ToString();
            if (_Regexp is null) return str;
            var match = _Regexp.Match(str);
            return match.Success
                ? (string.IsNullOrWhiteSpace(ReplacePattern) ? match.Value : match.Result(ReplacePattern))
                : null;
        }
    }
}