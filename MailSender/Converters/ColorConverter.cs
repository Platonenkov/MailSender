using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using MathCore.Annotations;

namespace MailSender.Converters
{
    [ValueConversion(typeof(Color), typeof(Color))]
    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    [MarkupExtensionReturnType(typeof(ColorConverter))]
    public class ColorConverter : ValueConverter
    {
        public double Brightness { get; set; } = 1;

        public double Alpha { get; set; } = 1;

        public double Red { get; set; } = 1;

        public double Green { get; set; } = 1;

        public double Blue { get; set; } = 1;

        public double AlphaValue { get; set; }

        public byte? RedValue { get; set; }

        public byte? GreenValue { get; set; }

        public byte? BlueValue { get; set; }


        public static Color Convert(
            Color color, double Brightness,
            double Alpha, double Red, double Green, double Blue,
            double AlphaValue = -1, byte? RedValue = null, byte? GreenValue = null, byte? BlueValue = null)
        {
            if (RedValue.HasValue) color.R = (byte)RedValue;
            else if (Red >= 0 && Red < 1) color.ScR = (float)(color.ScR * Red);

            if (GreenValue.HasValue) color.G = (byte)GreenValue;
            else if (Green >= 0 && Green < 1) color.ScG = (float)(color.ScR * Green);

            if (BlueValue.HasValue) color.B = (byte)BlueValue;
            else if (Blue >= 0 && Blue < 1) color.ScB = (float)(color.ScR * Blue);

            if (AlphaValue >= 0 && AlphaValue <= 1) color.A = (byte)(AlphaValue * 255);
            else if (Alpha >= 0 && Alpha < 1) color.ScA = (float)(color.ScA * Alpha);

            if (Brightness >= 0 && Brightness < 1)
            {
                color.ScR = (float)(color.ScR * Brightness);
                color.ScG = (float)(color.ScG * Brightness);
                color.ScB = (float)(color.ScB * Brightness);
            }

            return color;
        }

        public Color Convert(Color color) => Convert(color, Brightness, Alpha, Red, Green, Blue, AlphaValue, RedValue, GreenValue, BlueValue);
        public Color ConvertBack(Color color) => Convert(color, 1 - Brightness, 1 - Alpha, 1 - Red, 1 - Green, 1 - Blue);

        public Brush Convert([NotNull] SolidColorBrush brush)
        {
            if (brush.IsFrozen) brush = brush.CloneCurrentValue();
            brush.Color = Convert(brush.Color);
            return brush;
        }

        public Brush ConvertBack([NotNull] SolidColorBrush brush)
        {
            if (brush.IsFrozen) brush = brush.CloneCurrentValue();
            brush.Color = ConvertBack(brush.Color);
            return brush;
        }

        #region Overrides of ValueConverter

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            switch (v)
            {
                case null: return null;
                case Color color: return Convert(color);
                case SolidColorBrush brush: return Convert(brush);
            }
            throw new NotSupportedException("");
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            switch (v)
            {
                case null: return null;
                case Color color: return ConvertBack(color);
                case SolidColorBrush brush: return ConvertBack(brush);
            }
            throw new NotSupportedException("");
        }

        #endregion
    }
}
