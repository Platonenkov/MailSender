using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace MailSender.Classes
{
    [Serializable]
    public class SendersList
    {
        public ObservableCollection<Sender> Senders { get; set; }

        public SendersList() { Senders = new ObservableCollection<Sender>(); }

        public void Add(Sender sender) => Senders.Add(sender);
        public void Delete(Sender sender) => Senders.Remove(sender);

        public void Save(string FileName)
        {
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                var XML =new XmlSerializer(typeof(SendersList));
                XML.Serialize(stream,this);
            }
        }

        public static SendersList LoadFromFile(string FileName)
        {
            if(File.Exists(FileName))
                using (var stream = new FileStream(FileName, FileMode.Open))
                {
                    var XML = new XmlSerializer(typeof(SendersList));
                    if (stream.Length == 0) return new SendersList();
                    return (SendersList) XML.Deserialize(stream);
                }

            return null;
        }
    }
}