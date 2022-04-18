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
            listBox1.Items.Add("Dont touch");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            progLang = comboBox1.Text;
            pages = Convert.ToInt32(textBox1.Text);
            int ind = 0;
            lines = new string[16];

            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string str = client.DownloadString("https://www.amazon.com/s?k=" + comboBox1.Text + "&i=stripbooks-intl-ship&page=" + pages);


            Regex regex1 = new Regex("<span class=\"a-size-medium a-color-base a-text-normal\">(.*?)</span>");
            MatchCollection matches = regex1.Matches(str);
            //MatchCollection matches1 = regex1.Matches(line);
            Regex regex2 = new Regex("<a class=\"a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal\" href=\"(.*?)>");
            MatchCollection mathes = regex2.Matches(str);
            if(mathes.Count > 0)
            {
                foreach(Match match in mathes)
                {
                    string str2 = match.Groups[1].Value;
                    lines[ind] = str2;
                    ind++;
                }
            }

            if (matches.Count > 0)
            {
                foreach (Match match in matches){
                    string str2 = match.Groups[1].Value;
                    listBox1.Items.Add(str2);
                    
                    //lines[ind]=line;
                    //pages--;
                    //
                }//listBox1.Items.Add(match.Value);    
                            
                                                                
            }
            using (Stream stream = client.OpenRead("https://www.amazon.com/s?k=" + comboBox1.Text+ "&i=stripbooks-intl-ship&page=" + pages))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null )
                    {
                        
                       
                    }
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(lines != null)
            {
                //var line = lines[listBox1.SelectedIndex].Split('"');
                Process.Start(new ProcessStartInfo("https://www.amazon.com/"+lines[listBox1.SelectedIndex]) { UseShellExecute = true }); 
            }
                
            if(listBox1.SelectedItem.ToString() == "Dont touch")
                Process.Start(new ProcessStartInfo(@"https://www.youtube.com/watch?v=DLzxrzFCyOs&ab_channel=AllKindsOfStuff") { UseShellExecute = true });
            //System.Diagnostics.Process.Start(line[1]);
            
                
        }
    }
}
