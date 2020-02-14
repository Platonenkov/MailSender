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
        public string Position { get; set; }
        public string Company { get; set; }
        public string Occupation { get; set; }
        public string INN { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }


        private bool _WasSent;
        public bool WasSent
        {
            get => _WasSent;
            set
            {
                _WasSent = value;
                if(value) SendDateTime = DateTime.Now;
            }
        }

        public DateTime? SendDateTime { get; set; }
    }
}