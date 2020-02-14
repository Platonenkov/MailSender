using System;

namespace MailSender.Classes
{
    /// <summary>
    /// получатель писем
    /// </summary>
    [Serializable]
    public class Recipient : MathCore.ViewModels.ViewModel
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
                Set(ref _WasSent, value);
                if(value) SendDateTime = DateTime.Now;
                if(!value) SendDateTime = null;
            }
        }

        #region SendDateTime : DateTime? - Дата отправки

        /// <summary>Дата отправки</summary>
        private DateTime? _SendDateTime;

        /// <summary>Дата отправки</summary>
        public DateTime? SendDateTime { get => _SendDateTime; set => Set(ref _SendDateTime, value); }

        #endregion
    }
}