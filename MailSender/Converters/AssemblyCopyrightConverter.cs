using System.Reflection;

namespace MailSender.Converters
{
    public class AssemblyCopyrightConverter : AssemblyConverter
    {
        public AssemblyCopyrightConverter() : base(GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright)) { }
    }
}