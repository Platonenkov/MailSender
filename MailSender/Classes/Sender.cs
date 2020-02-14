using System;
using System.Windows.Documents;

namespace MailSender.Classes
{
    /// <summary>
    /// отправитель писем
    /// </summary>
    [Serializable]
    public class Sender:MathCore.ViewModels.ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string SmtpServer { get; set; }

        public bool EnableSsl { get; set; }

        public int Port { get; set; }


        #region Password : string - Пароль

        /// <summary>Пароль</summary>
        private string _Password;

        /// <summary>Пароль</summary>
        public string Password { get => _Password; set => Set(ref _Password, value); }

        #endregion

        private int _CountPer24Hours;
        public int CountPer24Hours
        {
            get
            {
                if (FirstInDay != null)
                {
                    var time = DateTime.Now - FirstInDay.Value;
                    if(time < TimeSpan.FromHours(24)) return _CountPer24Hours;
                    else _CountPer24Hours = 0;
                }
                return _CountPer24Hours;
            }
            set
            {
                Set(ref _CountPer24Hours, value);
                if(value == 1) FirstInDay = DateTime.Now;
            }
        }

        public DateTime? FirstInDay { get; set; }
    }
}
