using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

namespace FileManager1
{
    public partial class BookSite : Form
    {
        string progLang;
        int pages;
        string[] lines;

        public BookSite()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            progLang = comboBox1.Text;
            pages = Convert.ToInt32(textBox1.Text);
            int ind = 0;
            lines = new string[pages];

            WebClient client = new WebClient();

            using (Stream stream = client.OpenRead("https://www.packtpub.com/catalogsearch/result/?q="+ comboBox1.Text+ "&released=Available&page="+ pages))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null )
                    {
                        if(pages <= 0)
                        {
                            break;
                        }
                        //Regex regex1 = new Regex(comboBox1.Text);
                        Regex regex1 = new Regex("a href");
                        MatchCollection matches1 = regex1.Matches(line);
                        if (matches1.Count > 0)
                        {
                            foreach (Match match in matches1){
                                listBox1.Items.Add(line);
                                lines[ind]=line;
                                pages--;
                                ind++;
                            }//listBox1.Items.Add(match.Value);                                        
                        }
                       
                    }
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var line = lines[listBox1.SelectedIndex].Split('"');
            Process.Start(@"https://www.youtube.com");
            //System.Diagnostics.Process.Start(line[1]);
            using (FileStream file = new FileStream("C:\\Новая папка\\log.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(lines[listBox1.SelectedIndex]);
                file.WriteAsync(buffer);//
                file.Close();
            }
                
        }
    }
}
