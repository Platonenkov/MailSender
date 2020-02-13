using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using MailSender.Classes;
using MailSender.Command;

namespace MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Senders : SenderList - Список отправителей

        /// <summary>Список отправителей</summary>
        private SendersList _Senders;

        /// <summary>Список отправителей</summary>
        public SendersList Senders { get => _Senders; set => Set(ref _Senders, value); }

        #endregion

        #region Recipients : RecipientsList - Список получателей

        /// <summary>Список получателей</summary>
        private RecipientsList _Recipients;

        /// <summary>Список получателей</summary>
        public RecipientsList Recipients { get => _Recipients; set => Set(ref _Recipients, value); }

        #endregion

        #region SelectedSender : Sender - Выбранный Отправитель

        /// <summary>Выбранный Отправитель</summary>
        private Sender _SelectedSender;

        /// <summary>Выбранный Отправитель</summary>
        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }


        #endregion

        #region SelectedRecipient : Recipient - Выбранный отправитель

        /// <summary>Выбранный получатель</summary>
        private Recipient _SelectedRecipient;

        /// <summary>Выбранный получатель</summary>
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        #endregion


        #region Message : FlowDocument - Текст сообщения

        /// <summary>Текст сообщения</summary>
        private FlowDocument _Message;

        /// <summary>Текст сообщения</summary>
        public FlowDocument Message { get => _Message; set => Set(ref _Message, value); }

        #endregion

        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string RecipientsFile = $"Data\\Recipients.info";
        private static readonly string SendersFile = $"Data\\Senders.info";
        private static readonly string MessageFile = $"Data\\Message.info";
        private readonly string RecipientsFilePath = Path.Combine(CurrentDirectory, RecipientsFile);
        private readonly string SendersFilePath = Path.Combine(CurrentDirectory, SendersFile);
        private readonly string MessageFilePath = Path.Combine(CurrentDirectory, SendersFile);


        public MainViewModel()
        {
            Senders = new SendersList();
            Recipients = new RecipientsList();
            LoadRecipientsCommand = new LamdaCommand(LoadRecipientsFromFile, CanLoadRecipientsFile);
            LoadSendersCommand = new LamdaCommand(LoadSendersFromFile, CanLoadSendersFile);
            WindowClosingCommand = new LamdaCommand(SaveData);
            AddNewSenderCommand= new LamdaCommand(AddNewSenderClick);
            SaveSenderCommand = new GalaSoft.MvvmLight.Command.RelayCommand<Sender>(SaveSenderClick);
            DeleteSenderCommand = new LamdaCommand(DeleteSenderClick);
            AddNewRecipientCommand=new LamdaCommand(AddNewRecipientClick);
            SaveRecipientCommand = new GalaSoft.MvvmLight.Command.RelayCommand<Recipient>(SaveRecipientClick);
            DeleteRecipientCommand = new LamdaCommand(DeleteRecipientClick);
        }


        #region Commands

        public ICommand WindowClosingCommand { get; }
        public ICommand LoadRecipientsCommand { get; }
        public ICommand LoadSendersCommand { get; }
        public ICommand AddNewSenderCommand { get; }
        public ICommand SaveSenderCommand { get; }
        public ICommand DeleteSenderCommand { get; }
        public ICommand AddNewRecipientCommand { get; }
        public ICommand SaveRecipientCommand { get; }
        public ICommand DeleteRecipientCommand { get; }

        #region Recipients

        private void DeleteRecipientClick(object Obj)
        {
            if (SelectedRecipient is null) return;
            Recipients.Delete(SelectedRecipient);
        }

        private void SaveRecipientClick(Recipient recipient)
        {
            var data = recipient ?? SelectedRecipient;
            if (data is null) return;
            if(Recipients.Recipients.Contains(data))return;
            if(data.Name.IsNullOrWhiteSpace()&& data.Address.IsNullOrWhiteSpace()) return;

            Recipients.Add(data);

        }

        private void AddNewRecipientClick(object Obj)
        {
            SelectedRecipient = Recipients.Recipients.Count == 0 ? new Recipient{Id = 1} : new Recipient{Id = Recipients.Recipients[Recipients.Recipients.Count-1].Id + 1};
        }

        #endregion

        #region Senders

        private void DeleteSenderClick(object obj)
        {
            if(SelectedSender is null)return;
            Senders.Delete(SelectedSender);
        }

        private void SaveSenderClick(Sender sender)
        {
            var data = sender ?? SelectedSender;
            if (data is null) return;
            if(Senders.Senders.Contains(data))return;
            if(data.Name.IsNullOrWhiteSpace()||data.Address.IsNullOrWhiteSpace()||data.SmtpServer.IsNullOrWhiteSpace()||data.Password.IsNullOrWhiteSpace())return;

            Senders.Add(data);
        }

        private void AddNewSenderClick(object Obj)
        {
            SelectedSender = Senders.Senders.Count == 0 ? new Sender { Id = 1 } : new Sender { Id = Senders.Senders[Senders.Senders.Count - 1].Id + 1 };
        }

        #endregion

        #region Save|Load data from file

        private void SaveData(object Obj)
        {
            Senders.Save(SendersFilePath);
            Recipients.Save(RecipientsFile);
            SaveMessage();
        }

        private void SaveMessage()
        {
            using (var stream = new FileStream(MessageFilePath, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(FlowDocument));
                XML.Serialize(stream, Message);
            }

        }
        private bool CanLoadSendersFile(object Arg)
        {
            var filePath = SendersFilePath;
            if (File.Exists(filePath))
                return true;
            return CreateDefaultFile(filePath);

        }

        private void LoadSendersFromFile(object Obj)
        {
            var filePath = SendersFilePath;
            Senders = SendersList.LoadFromFile(filePath);
        }

        private void LoadRecipientsFromFile(object Obj)
        {
            var filePath = RecipientsFilePath;
            Recipients = RecipientsList.LoadFromFile(filePath);
        }

        private bool CanLoadRecipientsFile(object Arg)
        {
            var filePath = RecipientsFilePath;
            if (File.Exists(filePath))
                return true;
            return CreateDefaultFile(filePath);
        }

        private bool CreateDefaultFile(string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                if (Directory.Exists(file.Directory.FullName))
                {
                    using (_ = new FileStream(filePath, FileMode.Create)) { }
                    return true;
                }
                else
                {
                    Directory.CreateDirectory(file.Directory.FullName);
                    using (_ = new FileStream(filePath, FileMode.Create)) { }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #endregion
        #endregion
    }
}