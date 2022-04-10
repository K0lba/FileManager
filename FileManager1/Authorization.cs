using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileManager1
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }  

        public Authorization()
        {    
            Authorization authorization = null;
            if (File.Exists("log.xml"))
            {
                using (FileStream fs = new FileStream("log.xml", FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(Authorization));
                    authorization = (Authorization)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else
            {
                new FileStream("log.xml", FileMode.Create);
                authorization = new Authorization();
            }

            /*var xmlFormater = new XmlSerializer(typeof(Authorization));
            using (var file = new FileStream("log.xml", FileMode.OpenOrCreate))
            {
                xmlFormater.Serialize(file, this);
            }
            using (var file = new FileStream("log.xml", FileMode.OpenOrCreate))
            {
               var newData = xmlFormater.Deserialize(file) as Authorization;
                
            }*/
        }
        public void Save()
        {
            string filename = "log.xml";

            if (File.Exists(filename)) File.Delete(filename);


            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xser = new XmlSerializer(typeof(Authorization));
                xser.Serialize(fs, this);
                fs.Close();
            }
        }

    }
}
