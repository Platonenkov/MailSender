using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyFileVersionConverter : AssemblyConverter
    {
        public AssemblyFileVersionConverter() : base(GetAttributeValue<AssemblyFileVersionAttribute>(a => a.Version)) { }
    }
}