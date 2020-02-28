using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using GalaSoft.MvvmLight;
using MailSender.Classes;
using MailSender.Command;
using Notification.Wpf;

namespace MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        static NotificationManager notificationManager = new NotificationManager();

        #region Поиск

        #region FindByName : bool - Поиск по имени

        /// <summary>Поиск по имени</summary>
        private bool _FindByName;

        /// <summary>Поиск по имени</summary>
        public bool FindByName { get => _FindByName; set => Set(ref _FindByName, value); }

        #endregion

        #region FindByAddress : bool - поиск по адресу

        /// <summary>поиск по адресу</summary>
        private bool _FindByAddress;

        /// <summary>поиск по адресу</summary>
        public bool FindByAddress { get => _FindByAddress; set => Set(ref _FindByAddress, value); }

        #endregion

        #region FindById : bool - поиск по id

        /// <summary>поиск по id</summary>
        private bool _FindById;

        /// <summary>поиск по id</summary>
        public bool FindById { get => _FindById; set => Set(ref _FindById, value); }

        #endregion
        private string _FindString = string.Empty;
        public string FindString
        {
            get => _FindString;
            set
            {
                Set(ref _FindString, value);
                OnFindRowChanged();
            }

        }

        private void OnFindRowChanged()
        {
            if (FindString.IsNullOrWhiteSpace())
            { 
                FindRecipients = Recipients.Recipients;
                return;
            }

            var find = new ObservableCollection<Recipient>();
            foreach (var recipient in Recipients.Recipients)
            {
                if (FindByName && recipient.Name.IsNotNullOrWhiteSpace())
                {
                    if (Regular.FindString(FindString.ToLower(), recipient.Name.ToLower()))
                    {
                        find.Add(recipient);
                        continue;

                    }
                }
                if (FindByAddress && recipient.Address.IsNotNullOrWhiteSpace())
                    if (Regular.FindString(FindString.ToLower(), recipient.Address.ToLower()))
                    {
                        find.Add(recipient);
                        continue;

                    }
                if (FindById)
                    if (Regular.FindString(FindString.ToLower(), recipient.Id.ToString())) find.Add(recipient);
            }
            if (find.Count == 0) { FindRecipients = new ObservableCollection<Recipient>(); }
            else FindRecipients = find;

        }
        private ObservableCollection<Recipient> _FindRecipients;
        public ObservableCollection<Recipient> FindRecipients
        {
            get => _FindRecipients;
            set => Set(ref _FindRecipients, value);

        }


        #endregion


        #region Title : string - Title window

        /// <summary>Title window</summary>
        private string _Title ="SmapTool";

        /// <summary>Title window</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
        #region IsSenderWork : bool - Статус отправителя

        /// <summary>Статус отправителя</summary>
        private bool _IsSenderWork;

        /// <summary>Статус отправителя</summary>
        public bool IsSenderWork
        {
            get => _IsSenderWork;
            set
            {
                Set(ref _IsSenderWork, value);
                notificationManager.Show(null, value ? "Sending was started" : "Sending was stoped", NotificationType.Information);
            }
        }

        #endregion

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
        public RecipientsList Recipients
        {
            get => _Recipients;
            set
            {
                Set(ref _Recipients, value);
                FindRecipients = value.Recipients;
            }
        }

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


        #region MyHtmlProperty : string - текст редактора

        /// <summary>текст редактора</summary>
        private string _MyHtmlProperty;

        /// <summary>текст редактора</summary>
        public string MyHtmlProperty { get => _MyHtmlProperty; set => Set(ref _MyHtmlProperty, value); }

        #endregion
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string DataDirectory =Path.Combine(CurrentDirectory,$"Data");
        private static readonly string LogDirectory = Path.Combine(CurrentDirectory, $"Logs");

        private static readonly string LogFileName = $"Log.txt";
        private static readonly string RecipientsFile = $"Recipients.info";
        private static readonly string AttachFileName = $"AttachFileName.pdf";
        private static readonly string SendersFileName = $"Senders.info";
        private static readonly string MessageFileName = $"Message.info";

        private readonly string RecipientsFilePath = Path.Combine(CurrentDirectory, DataDirectory,RecipientsFile);
        private readonly string SendersFilePath = Path.Combine(CurrentDirectory, DataDirectory, SendersFileName);
        private readonly string MessageFilePath = Path.Combine(CurrentDirectory, DataDirectory, MessageFileName);
        private readonly string LogFilePath = Path.Combine(CurrentDirectory, LogDirectory, LogFileName);
        private readonly string AttachFilePath = Path.Combine(CurrentDirectory, DataDirectory, AttachFileName);


        private void CheckWorkDirectoryOrCreate()
        {
            if (!Directory.Exists(LogDirectory))
                try
                {
                    Directory.CreateDirectory(LogDirectory);
                }
                catch (Exception e)
                {
                    notificationManager.Show(null, $"Can't may create Logs Directory", NotificationType.Error);
                }
            if (!Directory.Exists(DataDirectory))
                try
                {
                    Directory.CreateDirectory(DataDirectory);
                }
                catch (Exception e)
                {
                    notificationManager.Show(null, $"Can't may create Data Directory", NotificationType.Error);
                }
        }

        private void CheckLogsOrMove()
        {
            if (File.Exists(LogFilePath))
            {
                var log = new FileInfo(LogFilePath);
                var newName = $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Log";
                newName = newName.Replace(".", String.Empty).Replace(":",String.Empty).Replace("\\",String.Empty).Replace("/",String.Empty);
                    
                log.MoveTo(Path.Combine(LogDirectory,newName));
            }
        }
        public MainViewModel()
        {

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            FindByName = true;
            FindByAddress = true;
            CheckWorkDirectoryOrCreate();
            CheckLogsOrMove();
            MyHtmlProperty = String.Empty;
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
            LoadMessageCommand = new LamdaCommand(ReadMessage);
            StartCommand= new LamdaCommand(StartSendMessage);
            StopCommand= new LamdaCommand(StopSendMessage);
        }

        private void StopSendMessage(object Obj) { IsSenderWork = false; }

        private void StartSendMessage(object Obj)
        {
            IsSenderWork = true;
            SendMessageAsync();
        }

        #region Pause : TimeSpan - Пауза между отправками

        /// <summary>Пауза между отправками</summary>
        private TimeSpan _Pause = new TimeSpan(0, 0, 2, 0);

        /// <summary>Пауза между отправками</summary>
        public TimeSpan Pause { get => _Pause; set => Set(ref _Pause, value); }

        #endregion

        #region MaxMailPer24Hour : int - Максимальное число в 24 часа

        /// <summary>Максимальное число в 24 часа</summary>
        private int _MaxMailPer24Hour = 500;

        /// <summary>Максимальное число в 24 часа</summary>
        public int MaxMailPer24Hour { get => _MaxMailPer24Hour; set => Set(ref _MaxMailPer24Hour, value); }

        #endregion
        private async void SendMessageAsync()
        {
            if(SelectedSender is null) return;

            foreach (var recipient in Recipients.Recipients.Where(r => r.WasSent == false))
            {
                if(recipient.Address.IsNullOrWhiteSpace())continue;
                if (SelectedSender.CountPer24Hours >= MaxMailPer24Hour)
                {
                    IsSenderWork = false;
                    notificationManager.Show("Info", "Limit of sending messages!!", NotificationType.Notification);
                    return;
                }
                var result =  await CreateMessageAsync(recipient).ConfigureAwait(true);

               if (result)
               {
                   recipient.WasSent = true;
                   recipient.StatusSending = "Отправлено";
                   SelectedSender.CountPer24Hours += 1;
               }

                await Task.Delay(Pause).ConfigureAwait(true);

               if(!IsSenderWork)return;
            }
            notificationManager.Show("Info", "Nothing to send", NotificationType.Notification);

            IsSenderWork = false;
        }

        public async Task<bool> CreateMessageAsync(Recipient recipient)
        {
            MailMessage msg = new MailMessage(SelectedSender.Address, recipient.Address) {Subject = "BuildRM, мы c Вами чтобы помогать.", Body = MyHtmlProperty };

            if(File.Exists(AttachFilePath))msg.Attachments.Add(new Attachment(AttachFilePath));
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(SelectedSender.SmtpServer, SelectedSender.Port)
            {
                Credentials = new NetworkCredential(SelectedSender.Address, SelectedSender.Password), EnableSsl = SelectedSender.EnableSsl
            };

            try
            {
               await smtp.SendMailAsync(msg).ConfigureAwait(true);
               notificationManager.Show("Info", "Message was successfully send", NotificationType.Notification);
               return true;
            }
            catch (Exception e)
            {
                try
                {
                    notificationManager.Show("Error", $"{e.ToString()}", NotificationType.Error, null, TimeSpan.MaxValue);

                    recipient.StatusSending = "Ошибка отправки (спам)";

                    if (!File.Exists(LogFilePath)) using (var writer = new FileStream(LogFilePath, FileMode.Create)) { }

                    using (StreamWriter sw = File.AppendText(LogFilePath))
                    {
                        sw.WriteLine(
                            "Exception caught in CreateTimeoutTestMessage(): {0}",$"{e.ToString()}, получатель {recipient.Address}, ID {recipient.Id}"
                        );
                    }
                }
                catch (Exception exception)
                {
                    notificationManager.Show("Error", $"{exception.ToString()}", NotificationType.Error, null, TimeSpan.MaxValue);

                }

                return false;
            }
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
        public ICommand LoadMessageCommand { get; }
        public ICommand StartCommand { get ; }
        public ICommand StopCommand { get ; }

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
            if(Recipients.Recipients.Contains(data))
            {
                notificationManager.Show(null, "There is this recipient", NotificationType.Warning);
                return;
            }
            if(data.Name.IsNullOrWhiteSpace() && data.Address.IsNullOrWhiteSpace())
            {
                notificationManager.Show(null, "You mast enter Name or Address", NotificationType.Warning);
                return;
            }

            Recipients.Add(data);
            notificationManager.Show(null, "Successfully", NotificationType.Success);

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
            notificationManager.Show(null, "Successfully", NotificationType.Success);
        }

        private void SaveSenderClick(Sender sender)
        {
            var data = sender ?? SelectedSender;
            if (data is null) return;
            if(Senders.Senders.Contains(data))
            {
                notificationManager.Show(null, "There is this sender", NotificationType.Warning);
                return;
            }
            if (data.Name.IsNullOrWhiteSpace()||data.Address.IsNullOrWhiteSpace()||data.SmtpServer.IsNullOrWhiteSpace()||data.Password.IsNullOrWhiteSpace())
            {
                notificationManager.Show(null, "You mast enter Name, Address, Smtp server, password", NotificationType.Warning);
                return;
            }

            Senders.Add(data);
            notificationManager.Show(null, "Successfully", NotificationType.Success);

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
            //chekspam();
            Recipients.Save(RecipientsFilePath);
            SaveMessage();
        }

        //private void chekspam()
        //{
        //    var spam = new int[]
        //    {
        //        89,91,106,112,115,117,118,119,129,136,136,143,148,149,156,159,166,171,173,175,176,177,178,180,181,182,184,186,189,190,191,197,198,199,203,208,
        //        210,211,212,221,226,227,228,229,234,236,237,238,239,240,247,249,250,262,264,265,267,268,271,272,273,274,278,279,283,286
        //    };

        //    foreach (var recipient in Recipients.Recipients)
        //    {
        //        if (recipient.Id <= 286 && recipient.Address.IsNotNullOrWhiteSpace() && !recipient.WasSent) recipient.WasSent = true;
        //        if (spam.Contains(recipient.Id)) recipient.WasSent = false;
        //    }

        //}
        private void SaveMessage()
        {
            using (StreamWriter sw = new StreamWriter(MessageFilePath))
            {
                sw.WriteLine(MyHtmlProperty);
            }
        }
        private void ReadMessage(object Obj)
        {
            if (!File.Exists(MessageFilePath)) return;
            using (StreamReader sr = new StreamReader(MessageFilePath))
            {
                MyHtmlProperty = sr.ReadToEnd();
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
                notificationManager.Show("Error", e.ToString(), NotificationType.Error, null, TimeSpan.MaxValue);

                return false;
            }
        }

        #endregion
        #endregion
    }
}