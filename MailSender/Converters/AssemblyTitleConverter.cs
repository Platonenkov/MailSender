using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyTitleConverter : AssemblyConverter
    {
        public AssemblyTitleConverter() : base(GetAttributeValue<AssemblyTitleAttribute>(a => a.Title)) { }
    }
}