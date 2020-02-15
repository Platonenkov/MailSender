using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyCompanyConverter : AssemblyConverter
    {
        public AssemblyCompanyConverter() : base(GetAttributeValue<AssemblyCompanyAttribute>(a => a.Company)) { }
    }
}