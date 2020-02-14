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

namespace MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region IsSenderWork : bool - Статус отправителя

        /// <summary>Статус отправителя</summary>
        private bool _IsSenderWork;

        /// <summary>Статус отправителя</summary>
        public bool IsSenderWork { get => _IsSenderWork; set => Set(ref _IsSenderWork, value); }

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


        #region Message : FlowDocument - Текст сообщения

        /// <summary>Текст сообщения</summary>
        private FlowDocument _Message;

        /// <summary>Текст сообщения</summary>
        public FlowDocument Message { get => _Message; set => Set(ref _Message, value); }

        #endregion
        #region TextMessage : string - text

        /// <summary>text</summary>
        private string _TextMessage;

        /// <summary>text</summary>
        public string TextMessage
        {
            get => _TextMessage;
            set
            {
                Set(ref _TextMessage, value);
                _HtmlMessage = null;
            }
        }

        #endregion
        #region HtmlMessage : string - HTML сообщение

        /// <summary>HTML сообщение</summary>
        private string _HtmlMessage;

        /// <summary>HTML сообщение</summary>
        public string HtmlMessage
        {
            get
            {
                if (_HtmlMessage != null) return _HtmlMessage;

                return _HtmlMessage = GetHtmlFromString(TextMessage);
            }
        }

        private string GetHtmlFromString(string text)
        {
            //The text will be loaded here
            string s2 = text;

            //All blank spaces would be replaced for html subsitute of blank space(&nbsp;) 
            s2 = s2.Replace(" ", "&nbsp;");

            //Carriage return & newline replaced to <br/>
            s2 = s2.Replace("\r\n", "<br/>");
            string Str = "<html>";
            Str += "<head>";
            Str += "<title></title>";
            Str += "</head>";
            Str += "<body>";
            Str += "<table border=0 width=95% cellpadding=0 cellspacing=0>";
            Str += "<tr>";
            Str += "<td>" + s2 + "</td>";
            Str += "</tr>";
            Str += "</table>";
            Str += "</body>";
            Str += "</html>";
            return Str;
        }
        #endregion

        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string RecipientsFile = $"Data\\Recipients.info";
        private static readonly string AttachFile = $"Data\\AttachFile.pdf";
        private static readonly string SendersFile = $"Data\\Senders.info";
        private static readonly string MessageFile = $"Data\\Message.info";
        private static readonly string LogFile = $"Data\\Log.info";
        private readonly string RecipientsFilePath = Path.Combine(CurrentDirectory, RecipientsFile);
        private readonly string SendersFilePath = Path.Combine(CurrentDirectory, SendersFile);
        private readonly string MessageFilePath = Path.Combine(CurrentDirectory, MessageFile);
        private readonly string LogFilePath = Path.Combine(CurrentDirectory, LogFile);
        private readonly string AttachFilePath = Path.Combine(CurrentDirectory, AttachFile);


        public MainViewModel()
        {
            Message = new FlowDocument();
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
            MailMessage msg = new MailMessage(SelectedSender.Address, recipient.Address) {Subject = "BuildRM, мы c Вами чтобы помогать.", Body = HtmlMessage};

            if(File.Exists(AttachFilePath))msg.Attachments.Add(new Attachment(AttachFilePath));
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(SelectedSender.SmtpServer, SelectedSender.Port)
            {
                Credentials = new NetworkCredential(SelectedSender.Address, SelectedSender.Password), EnableSsl = SelectedSender.EnableSsl
            };

            try
            {
               await smtp.SendMailAsync(msg).ConfigureAwait(true);
               return true;
            }
            catch (Exception e)
            {
                try
                {
                    if(!File.Exists(LogFilePath)) using (var writer = new FileStream(LogFilePath, FileMode.Create)) { }

                    using (StreamWriter sw = File.AppendText(LogFilePath))
                    {
                        sw.WriteLine("Exception caught in CreateTimeoutTestMessage(): {0}",
                            e.ToString());
                    }
                }
                catch (Exception exception)
                {
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
            if(Recipients.Recipients.Contains(data))return;
            if(data.Name.IsNullOrWhiteSpace() && data.Address.IsNullOrWhiteSpace()) return;

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
            using (StreamWriter sw = new StreamWriter(MessageFilePath))
            {
                sw.WriteLine(TextMessage);
            }
        }
        private void ReadMessage(object Obj)
        {
            if (!File.Exists(MessageFilePath)) return;
            using (StreamReader sr = new StreamReader(MessageFilePath))
            {
                TextMessage = sr.ReadToEnd();
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