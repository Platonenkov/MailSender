using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyProductConverter : AssemblyConverter
    {
        public AssemblyProductConverter() : base(GetAttributeValue<AssemblyProductAttribute>(a => a.Product)) { }
    }
}