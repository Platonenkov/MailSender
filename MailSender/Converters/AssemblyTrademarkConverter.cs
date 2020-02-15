using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyTrademarkConverter : AssemblyConverter
    {
        public AssemblyTrademarkConverter() : base(GetAttributeValue<AssemblyTrademarkAttribute>(a => a.Trademark)) { }
    }
}