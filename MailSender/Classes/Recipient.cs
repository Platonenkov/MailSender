using System;

namespace MailSender.Classes
{
    /// <summary>
    /// получатель писем
    /// </summary>
    [Serializable]
    public class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public bool WasSent { get; set; }

        public DateTime? SendDateTime { get; set; }
    }
}