using System.Net;
using System.Text.RegularExpressions;

namespace FileManager1
{
    public partial class BookSite : Form
    {
        string progLang;
        int pages;

        public BookSite()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progLang = comboBox1.Text;
            pages = Convert.ToInt32(textBox1.Text);

            WebClient client = new WebClient();

            using (Stream stream = client.OpenRead("https://www.packtpub.com/catalogsearch/result/?q="+ comboBox1.Text+ "&released=Available&page="+ pages))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null || pages>0)
                    {
                        
                        //Regex regex1 = new Regex(comboBox1.Text);
                        Regex regex1 = new Regex("a href");
                        MatchCollection matches1 = regex1.Matches(line);
                        if (matches1.Count > 0)
                        {
                            foreach (Match match in matches1){
                                listBox1.Items.Add(line);
                                pages--;
                            }//listBox1.Items.Add(match.Value);                                        
                        }
                       
                    }
                }
            }

        }

    }
}
