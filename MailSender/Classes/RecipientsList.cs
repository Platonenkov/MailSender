using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MailSender.Classes
{
    [Serializable]
    public class RecipientsList
    {
        public ObservableCollection<Recipient> Recipients { get; set; }

        public RecipientsList() { Recipients = new ObservableCollection<Recipient>(); }

        public void Add(Recipient recipient) => Recipients.Add(recipient);
        public void Delete(Recipient recipient) => Recipients.Remove(recipient);

        public void Save(string FileName)
        {
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                var XML =new XmlSerializer(typeof(RecipientsList));
                XML.Serialize(stream,this);
            }
        }

        public static RecipientsList LoadFromFile(string FileName)
        {
            if (File.Exists(FileName))
                using (var stream = new FileStream(FileName, FileMode.Open))
                {
                    var XML = new XmlSerializer(typeof(RecipientsList));
                    if(stream.Length == 0)return new RecipientsList();
                    return (RecipientsList) XML.Deserialize(stream);
                }

            return null;
        }
    }
}