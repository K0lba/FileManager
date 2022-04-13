using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileManager1
{
    [DataContract]
    public class Authorization
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Size { get; set; }

        [DataMember]
        public Color Color { get; set; }

        [DataMember]
        public string Font { get; set; }

        public static Authorization GetSettings()
        {
            Authorization settings = null;
            string filename = "log.json";

            //проверка наличия файла
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    var xser = new DataContractJsonSerializer(typeof(Authorization));
                    settings = (Authorization)xser.ReadObject(fs);
                    fs.Close();
                }
            }
            else
            {
                settings = new Authorization();
            }

            return settings;
        }

        [OnSerializing()]
        private void SetValuesOnSerializingMethod(StreamingContext context)
        {
            Login = Crypter.Encrypt(Login);
            Password = Crypter.Encrypt(Password);
        }

        [OnDeserialized()]
        internal void OnDeserializedMethodMethod(StreamingContext context)
        {
            Login = Crypter.Decrypt(Login);
            Password = Crypter.Decrypt(Password);
        }
        public void Save()
        {
            string filename = "log.json";
            if (File.Exists(filename)) File.Delete(filename);
            var xser = new DataContractJsonSerializer(typeof(Authorization));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                xser.WriteObject(fs, this);
                fs.Close();
            }
        }

        

    }
}
