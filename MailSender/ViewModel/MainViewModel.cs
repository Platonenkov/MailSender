using System;
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


        #region MyHtmlProperty : string - текст редактора

        /// <summary>текст редактора</summary>
        private string _MyHtmlProperty;

        /// <summary>текст редактора</summary>
        public string MyHtmlProperty { get => _MyHtmlProperty; set => Set(ref _MyHtmlProperty, value); }

        #endregion
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string RecipientsFile = $"Data\\Recipients.info";
        private static readonly string AttachFile = $"Data\\AttachFile.pdf";
        private static readonly string SendersFile = $"Data\\Senders.info";
        private static readonly string MessageFile = $"Data\\Message.info";
        private static readonly string LogFile = $"Data\\Log.txt";
        private readonly string RecipientsFilePath = Path.Combine(CurrentDirectory, RecipientsFile);
        private readonly string SendersFilePath = Path.Combine(CurrentDirectory, SendersFile);
        private readonly string MessageFilePath = Path.Combine(CurrentDirectory, MessageFile);
        private readonly string LogFilePath = Path.Combine(CurrentDirectory, LogFile);
        private readonly string AttachFilePath = Path.Combine(CurrentDirectory, AttachFile);


        public MainViewModel()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
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

        private  TimeSpan Pause = new TimeSpan(0,0,2,0);
        private async void SendMessageAsync()
        {
            if(SelectedSender is null) return;

            foreach (var recipient in Recipients.Recipients.Where(r => r.WasSent == false))
            {
                if(recipient.Address.IsNullOrWhiteSpace())continue;
                if (SelectedSender.CountPer24Hours >= 500)
                {
                    IsSenderWork = false;
                    return;
                }
                var result =  await CreateMessageAsync(recipient).ConfigureAwait(true);

               if (result)
               {
                   recipient.WasSent = true;
                   SelectedSender.CountPer24Hours += 1;
               }
               
               await Task.Delay(Pause).ConfigureAwait(true);

               if(!IsSenderWork)return;
            }
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
            Recipients.Save(RecipientsFile);
            SaveMessage();
        }

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