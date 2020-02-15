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

        #region Name : string - ФИО

        /// <summary>ФИО</summary>
        private string _Name;

        /// <summary>ФИО</summary>
        public string Name { get => _Name; set => Set(ref _Name, value); }

        #endregion
        #region Address : string - Адрес почты

        /// <summary>Адрес почты</summary>
        private string _Address;

        /// <summary>Адрес почты</summary>
        public string Address { get => _Address; set => Set(ref _Address, value); }

        #endregion
        #region SmtpServer : string - Smtp Server

        /// <summary>Smtp Server</summary>
        private string _SmtpServer;

        /// <summary>Smtp Server</summary>
        public string SmtpServer { get => _SmtpServer; set => Set(ref _SmtpServer, value); }

        #endregion
        #region EnableSsl : bool - вкл или нет SSL

        /// <summary>вкл или нет SSL</summary>
        private bool _EnableSsl;

        /// <summary>вкл или нет SSL</summary>
        public bool EnableSsl { get => _EnableSsl; set => Set(ref _EnableSsl, value); }

        #endregion

        #region Port : string - Порт

        /// <summary>Порт</summary>
        private int _Port;

        /// <summary>Порт</summary>
        public int Port { get => _Port; set => Set(ref _Port, value); }

        #endregion
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
                    else
                    {
                        _CountPer24Hours = 0;
                        FirstInDay = null;
                    }
                }
                return _CountPer24Hours;
            }
            set
            {
                Set(ref _CountPer24Hours, value);
                TotalSent++;
                var time = DateTime.Now;
                LastSend = time;
                if(value == 1) FirstInDay = time;
            }
        }

        #region FirstInDay : DateTime? - Первый за день


        #region TotalSent : long - Всего отправлено

        /// <summary>Всего отправлено</summary>
        private long _TotalSent;

        /// <summary>Всего отправлено</summary>
        public long TotalSent { get => _TotalSent; set => Set(ref _TotalSent, value); }

        #endregion
        #region LastSend : DateTime? - Дата последней отправки

        /// <summary>Дата последней отправки</summary>
        private DateTime? _LastSend;

        /// <summary>Дата последней отправки</summary>
        public DateTime? LastSend { get => _LastSend; set => Set(ref _LastSend, value); }

        #endregion
        /// <summary>Первый за день</summary>
        private DateTime? _FirstInDay;

        /// <summary>Первый за день</summary>
        public DateTime? FirstInDay { get => _FirstInDay; set => Set(ref _FirstInDay, value); }

        #endregion
    }
}
