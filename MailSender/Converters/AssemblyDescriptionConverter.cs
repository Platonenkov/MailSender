using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyDescriptionConverter : AssemblyConverter
    {
        public AssemblyDescriptionConverter() : base(GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description)) { }
    }
}