using System;
using System.Globalization;
using System.Windows.Markup;
using MathCore.Annotations;

// ReSharper disable UnusedMember.Global

namespace MailSender.Converters
{
    [System.Diagnostics.DebuggerStepThrough]
    [MarkupExtensionReturnType(typeof(MathConverter))]
    public class MathConverter : ValueConverter
    {
        #region Static fields
        [CanBeNull] private static MathConverter __Round;
        [CanBeNull] private static MathConverter __Truncate;
        [CanBeNull] private static MathConverter __Floor;
        [CanBeNull] private static MathConverter __Ceiling;
        [CanBeNull] private static MathConverter __Abs;
        [CanBeNull] private static MathConverter __Sign;
        [CanBeNull] private static MathConverter __Sin;
        [CanBeNull] private static MathConverter __Cos;
        [CanBeNull] private static MathConverter __Tg;
        [CanBeNull] private static MathConverter __Ctg;
        [CanBeNull] private static MathConverter __ArcSin;
        [CanBeNull] private static MathConverter __ArcCos;
        [CanBeNull] private static MathConverter __ArcTg;
        [CanBeNull] private static MathConverter __Sh;
        [CanBeNull] private static MathConverter __Ch;
        [CanBeNull] private static MathConverter __Tgh;
        [CanBeNull] private static MathConverter __Ctgh;
        [CanBeNull] private static MathConverter __Ln;
        [CanBeNull] private static MathConverter __Lg;
        [CanBeNull] private static MathConverter __Exp;
        [CanBeNull] private static MathConverter __Pow10;
        [CanBeNull] private static MathConverter __InPow2;
        [CanBeNull] private static MathConverter __Sqrt;
        [CanBeNull] private static MathConverter __db;
        [CanBeNull] private static MathConverter __dbPower;

        [CanBeNull] private static MathConverter __Negate;
        [CanBeNull] private static MathConverter __Inverse;
        [CanBeNull] private static MathConverter __Add;
        [CanBeNull] private static MathConverter __Substract;
        [CanBeNull] private static MathConverter __SubstractFrom;
        [CanBeNull] private static MathConverter __Multiply;
        [CanBeNull] private static MathConverter __Divade;
        [CanBeNull] private static MathConverter __DivadeFrom;
        [CanBeNull] private static MathConverter __Div;
        [CanBeNull] private static MathConverter __Mod;
        [CanBeNull] private static MathConverter __PowerTo;
        [CanBeNull] private static MathConverter __PowerOf;
        [CanBeNull] private static MathConverter __LogOnBase;
        [CanBeNull] private static MathConverter __LogOfBase;
        #endregion

        #region Static Properties
        [NotNull] public static MathConverter Round => __Round ?? (__Round = new MathConverter((v, n) => Math.Round(v, n ?? 0)));
        [NotNull] public static MathConverter Truncate => __Truncate ?? (__Truncate = new MathConverter(Math.Truncate));
        [NotNull] public static MathConverter Floor => __Floor ?? (__Floor = new MathConverter(Math.Floor));
        [NotNull] public static MathConverter Ceiling => __Ceiling ?? (__Ceiling = new MathConverter(Math.Ceiling));
        [NotNull] public static MathConverter Abs => __Abs ?? (__Abs = new MathConverter(Math.Abs));
        [NotNull] public static MathConverter Sign => __Sign ?? (__Sign = new MathConverter(v => Math.Sign(v)));
        [NotNull] public static MathConverter Sin => __Sin ?? (__Sin = new MathConverter(Math.Sin, Math.Asin));
        [NotNull] public static MathConverter Cos => __Cos ?? (__Cos = new MathConverter(Math.Cos, Math.Acos));
        [NotNull] public static MathConverter Tg => __Tg ?? (__Tg = new MathConverter(Math.Tan, Math.Atan));
        [NotNull] public static MathConverter Ctg => __Ctg ?? (__Ctg = new MathConverter(v => 1 / Math.Tan(v), v => Math.Atan(1 / v)));
        [NotNull] public static MathConverter ArcSin => __ArcSin ?? (__ArcSin = new MathConverter(Math.Asin, Math.Sin));
        [NotNull] public static MathConverter ArcCos => __ArcCos ?? (__ArcCos = new MathConverter(Math.Acos, Math.Cos));
        [NotNull] public static MathConverter ArcTg => __ArcTg ?? (__ArcTg = new MathConverter(Math.Atan, Math.Tan));
        [NotNull] public static MathConverter Sh => __Sh ?? (__Sh = new MathConverter(Math.Sinh, v => Math.Log(v + Math.Sqrt(v * v + 1))));
        [NotNull] public static MathConverter Ch => __Ch ?? (__Ch = new MathConverter(Math.Cosh, v => Math.Log(v + Math.Sqrt(v * v - 1))));
        [NotNull] public static MathConverter Tgh => __Tgh ?? (__Tgh = new MathConverter(Math.Tanh, v => Math.Log((1 + v) / (1 - v)) / 2));
        [NotNull] public static MathConverter Ctgh => __Ctgh ?? (__Ctgh = new MathConverter(v => 1 / Math.Tanh(v), v => Math.Log((v + 1) / (v - 1)) / 2));
        [NotNull] public static MathConverter Ln => __Ln ?? (__Ln = new MathConverter(Math.Log, Math.Exp));
        [NotNull] public static MathConverter Lg => __Lg ?? (__Lg = new MathConverter(Math.Log10, v => Math.Pow(10, v)));
        [NotNull] public static MathConverter Exp => __Exp ?? (__Exp = new MathConverter(Math.Exp, Math.Log));
        [NotNull] public static MathConverter Pow10 => __Pow10 ?? (__Pow10 = new MathConverter(v => Math.Pow(10, v), Math.Log10));
        [NotNull] public static MathConverter InPow2 => __InPow2 ?? (__InPow2 = new MathConverter(v => v * v, Math.Sqrt));
        [NotNull] public static MathConverter Sqrt => __Sqrt ?? (__Sqrt = new MathConverter(Math.Sqrt, v => v * v));
        [NotNull] public static MathConverter db => __db ?? (__db = new MathConverter(v => 20 * Math.Log10(v), v => Math.Pow(10, v / 20)));
        [NotNull] public static MathConverter dbPower => __dbPower ?? (__dbPower = new MathConverter(v => 10 * Math.Log10(v), v => Math.Pow(10, v / 10)));
        [NotNull] public static MathConverter Negate => __Negate ?? (__Negate = new MathConverter(v => -v, v => -v));
        [NotNull] public static MathConverter Inverse => __Inverse ?? (__Inverse = new MathConverter(v => 1 / v, v => 1 / v));
        [NotNull] public static MathConverter Add => __Add ?? (__Add = new MathConverter((double v, double? p) => v + (p ?? 0d), (v, p) => v - (p ?? 0d)));
        [NotNull] public static MathConverter Substract => __Substract ?? (__Substract = new MathConverter((double v, double? p) => v - (p ?? 0d), (v, p) => v + (p ?? 0d)));
        [NotNull] public static MathConverter SubstractFrom => __SubstractFrom ?? (__SubstractFrom = new MathConverter((double v, double? p) => (p ?? 0d) - v, (v, p) => (p ?? 0d) + v));
        [NotNull] public static MathConverter Multiply => __Multiply ?? (__Multiply = new MathConverter((double v, double? p) => v * (p ?? 1d), (v, p) => v / (p ?? 1d)));
        [NotNull] public static MathConverter Divade => __Divade ?? (__Divade = new MathConverter((double v, double? p) => v / (p ?? 1d), (v, p) => v * (p ?? 1d)));
        // ReSharper disable once PossibleLossOfFraction
        [NotNull] public static MathConverter Div => __Div ?? (__Div = new MathConverter((double v, int? p) => (int)v / (p ?? 1)));
        [NotNull] public static MathConverter Mod => __Mod ?? (__Mod = new MathConverter((double v, double? p) => v % (p ?? 1d)));
        [NotNull] public static MathConverter DivadeFrom => __DivadeFrom ?? (__DivadeFrom = new MathConverter((double v, double? p) => (p ?? 1d) / v, (v, p) => (p ?? 1d) * v));
        [NotNull] public static MathConverter PowerTo => __PowerTo ?? (__PowerTo = new MathConverter((double v, double? p) => Math.Pow(v, p ?? 1), (v, p) => Math.Pow(v, 1 / (p ?? 1))));
        [NotNull] public static MathConverter PowerOf => __PowerOf ?? (__PowerOf = new MathConverter((double v, double? p) => Math.Pow(p ?? 1, v), (v, p) => Math.Log(v, p ?? 1)));
        [NotNull] public static MathConverter LogOnBase => __LogOnBase ?? (__LogOnBase = new MathConverter((double v, double? p) => Math.Log(v, p ?? 1), (v, p) => Math.Pow(p ?? 1, v)));
        [NotNull] public static MathConverter LogOfBase => __LogOfBase ?? (__LogOfBase = new MathConverter((double v, double? p) => Math.Log(p ?? 1, v), (v, p) => Math.Pow(v, p ?? 1)));
        #endregion

        [NotNull] protected readonly Func<object, Type, object, CultureInfo, object> _To;
        [NotNull] protected readonly Func<object, Type, object, CultureInfo, object> _From;

        protected MathConverter() { }

        protected MathConverter([NotNull] Func<double, double> To, [CanBeNull] Func<double, double> From = null)
            : this
            (
                (v, t, p, c) => To(DoubleValueConverter.ToDouble(v)),
                From is null
                    ? new Func<object, Type, object, CultureInfo, object>(
                        (v, t, p, c) => throw new NotSupportedException("Обратное преобразование не поддерживается"))
                    : (v, t, p, c) => From(DoubleValueConverter.ToDouble(v))
            )
        {
        }

        protected MathConverter
        (
            [NotNull] Func<double, double?, double> To,
            [CanBeNull] Func<double, double?, double> From = null
        )
            : this
            (
                (v, t, p, c) => To(DoubleValueConverter.ToDouble(v), p as double?),
                From is null
                    ? new Func<object, Type, object, CultureInfo, object>(
                        (v, t, p, c) => throw new NotSupportedException("Обратное преобразование не поддерживается"))
                    : ((v, t, p, c) => From(DoubleValueConverter.ToDouble(v), p as double?))
            )
        {
        }

        protected MathConverter
        (
            [NotNull] Func<double, int?, double> To,
            [CanBeNull] Func<double, int?, double> From = null
        )
            : this
            (
                (v, t, p, c) => To(DoubleValueConverter.ToDouble(v), p as int?),
                From is null
                    ? new Func<object, Type, object, CultureInfo, object>(
                        (v, t, p, c) => throw new NotSupportedException("Обратное преобразование не поддерживается"))
                    : ((v, t, p, c) => From(DoubleValueConverter.ToDouble(v), p as int?))
            )
        {
        }

        protected MathConverter
        (
            [NotNull] Func<object, Type, object, CultureInfo, object> To,
            [NotNull] Func<object, Type, object, CultureInfo, object> From
        )
        {
            _To = To;
            _From = From;
        }

        public override object Convert(object v, Type t, object p, CultureInfo c) => _To(v, t, p, c);

        public override object ConvertBack(object v, Type t, object p, CultureInfo c) => _From(v, t, p, c);
    }
}