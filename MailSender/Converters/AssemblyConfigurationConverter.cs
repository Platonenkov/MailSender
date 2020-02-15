using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyConfigurationConverter : AssemblyConverter
    {
        public AssemblyConfigurationConverter() : base(GetAttributeValue<AssemblyConfigurationAttribute>(a => a.Configuration)) { }
    }
}