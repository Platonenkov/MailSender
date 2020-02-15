namespace MailSender.Converters
{
    public class AssemblyVersionConverter : AssemblyConverter
    {
        public AssemblyVersionConverter() : base(a => a.GetName().Version) { }
    }
}