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
        
        #region Name : string - ФИО

        /// <summary>ФИО</summary>
        private string _Name;

        /// <summary>ФИО</summary>
        public string Name { get => _Name; set => Set(ref _Name, value); }

        #endregion
        #region Position : string - Должность

        /// <summary>Должность</summary>
        private string _Position;

        /// <summary>Должность</summary>
        public string Position { get => _Position; set => Set(ref _Position, value); }

        #endregion
        #region Company : string - название компании

        /// <summary>название компании</summary>
        private string _Company;

        /// <summary>название компании</summary>
        public string Company { get => _Company; set => Set(ref _Company, value); }

        #endregion
        #region Occupation : string - Род деятельности

        /// <summary>Род деятельности</summary>
        private string _Occupation;

        /// <summary>Род деятельности</summary>
        public string Occupation { get => _Occupation; set => Set(ref _Occupation, value); }

        #endregion
        #region INN : string - ИНН

        /// <summary>ИНН</summary>
        private string _INN;

        /// <summary>ИНН</summary>
        public string INN { get => _INN; set => Set(ref _INN, value); }

        #endregion
        #region Phone : string - Телефон

        /// <summary>Телефон</summary>
        private string _Phone;

        /// <summary>Телефон</summary>
        public string Phone { get => _Phone; set => Set(ref _Phone, value); }

        #endregion
        #region Address : string - адрес почты

        /// <summary>адрес почты</summary>
        private string _Address;

        /// <summary>адрес почты</summary>
        public string Address { get => _Address; set => Set(ref _Address, value); }

        #endregion

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

        #region InterestIsShown : bool - Проявлен интерес

        /// <summary>Проявлен интерес</summary>
        private bool _interestIsShown;

        /// <summary>Проявлен интерес</summary>
        public bool InterestIsShown
        {
            get => _interestIsShown;
            set
            {
                Set(ref _interestIsShown, value);
                if (value) StatusSending = "Проявлен интерес";
                else StatusSending = string.Empty;
            }
        }

        #endregion

        #region StatusSending : string - статус отправки

        /// <summary>статус отправки</summary>
        private string _StatusSending;

        /// <summary>статус отправки</summary>
        public string StatusSending { get => _StatusSending; set => Set(ref _StatusSending, value); }

        #endregion
    }
}