using System;
using System.Windows.Documents;

namespace MailSender.Classes
{
    /// <summary>
    /// отправитель писем
    /// </summary>
    [Serializable]
    public class Sender
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string SmtpServer { get; set; }

        public bool EnableSsl { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public int CountPer24Hours { get; set; }

        public DateTime? FirstInDay { get; set; }

    }
}
