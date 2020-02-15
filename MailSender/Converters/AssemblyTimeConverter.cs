using System.IO;

namespace MailSender.Converters
{
    public class AssemblyTimeConverter : AssemblyConverter
    {
        public AssemblyTimeConverter() : base(a => new FileInfo(a.Location).CreationTime) { }
    }
}