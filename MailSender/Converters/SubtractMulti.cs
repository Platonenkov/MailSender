using System.Windows.Markup;

namespace MailSender.Converters
{
    [MarkupExtensionReturnType(typeof(SubtractMulti))]
    public class SubtractMulti : MultiDoubleConverter
    {
        protected override double To(double[] vv, object p)
        {
            if (!(vv?.Length > 0)) return double.NaN;
            var result = vv[0];
            for (var i = 1; i < vv.Length; i++)
                result -= vv[i];
            return result;
        }
    }

    [MarkupExtensionReturnType(typeof(SubtractNegativeToNanMulti))]
    public class SubtractNegativeToNanMulti : MultiDoubleConverter
    {
        protected override double To(double[] vv, object p)
        {
            if (!(vv?.Length > 0)) return double.NaN;
            var result = vv[0];
            for (var i = 1; i < vv.Length; i++)
                result -= vv[i];
            return result < 0 ? double.NaN : result;
        }
    }
}