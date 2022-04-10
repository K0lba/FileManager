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
        public static Authorization GetSettings()
        {
            Authorization settings = null;
            string filename = "log.xml";

            //проверка наличия файла
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(Authorization));
                    settings = (Authorization)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else
            {
                settings = new Authorization();
            }

            return settings;
        }


        public void Save()
        {
            string filename = "log.xml";
            if (File.Exists(filename)) File.Delete(filename);
            XmlSerializer xser = new XmlSerializer(typeof(Authorization));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                xser.Serialize(fs, this);
                fs.Close();
            }
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public void SetLogin(string text)
        {
            Login = Crypter.Encrypt(text);
        }
        public string GetLogin()
        {
            string res;
            try
            {
                res = Crypter.Decrypt(Login);
            }
            catch
            {
                res = Login;
            }
            return res;
        }
        public void SetPassword(string text)
        {
            Password = Crypter.Encrypt(text);
        }
        public string GetPassword()
        {
            string res;
            try
            {
                res = Crypter.Decrypt(Password);
            }
            catch
            {
                res = Password;
            }
            return res;
        }




    }
}
