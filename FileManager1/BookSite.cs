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
            int count = 1;
            lines = new string[pages];

            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string str = client.DownloadString("https://www.amazon.com/s?k=" + comboBox1.Text + "&i=stripbooks-intl-ship&page=");
            

            Regex regexName = new Regex("<span class=\"a-size-medium a-color-base a-text-normal\">(.*?)</span>");
            //MatchCollection matches = regexName.Matches(str);
            Regex regexDate = new Regex("<span class=\"a-size-base a-color-secondary a-text-normal\">(.*?)</span>");
            //MatchCollection matchesDate = regexDate.Matches(str);
            Regex regexAuthor = new Regex("<a class=\"a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style\" href=\"(.*?)\">(.*?)</a>");
            //MatchCollection matchesAuthor = regexAuthor.Matches(str);
            //MatchCollection matches1 = regex1.Matches(line);
            Regex regexLink = new Regex("<a class=\"a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal\" href=\"(.*?)>");
            //MatchCollection mathes = regexLink.Matches(str);
            

            /*if (matches.Count > 0)
            {
                for(int i=0; i<matches.Count;i++){
                    string str2 = matches[i].Groups[1].Value;
                    string str3 = matchesDate[i].Groups[1].Value;
                    string str4 = matchesAuthor[i].Groups[2].Value;
                    listBox1.Items.Add(str2+" "+str4+" "+str3);
                    
                    //lines[ind]=line;
                    //pages--;
                    //
                }//listBox1.Items.Add(match.Value);    
                            
                                                                
            }*/
            Go:
            using (Stream stream = client.OpenRead("https://www.amazon.com/s?k=" + comboBox1.Text+ "&i=stripbooks-intl-ship&page="+count))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null )
                    {
                        if (pages <= 0)
                        {
                            break;
                        }
                        MatchCollection matches = regexName.Matches(line);
                        if(matches.Count > 0)
                        {
                            MatchCollection matchesDate = regexDate.Matches(line);
                            MatchCollection matchesAuthor = regexAuthor.Matches(line);
                            if( matchesDate.Count > 0 && matchesAuthor.Count > 0)
                            {
                                for(int i = 0; i < matchesDate.Count; i++)
                                {
                                    string str2 = matches[i].Groups[1].Value;
                                    string str3 = matchesDate[i].Groups[1].Value;
                                    string str4 = matchesAuthor[i].Groups[2].Value;
                                    listBox1.Items.Add(str2 + " by " + str4 + " DATE: " + str3);
                                }
                            }
                            else if(matchesDate.Count > 0)
                            {
                                //Regex newAuthor = new Regex("<span class=\"a-size-base\">(.*?)</span>");
                                //MatchCollection matchesAuthor2 = regexAuthor.Matches(line);
                                for (int i = 0; i < matchesDate.Count; i++)
                                {
                                    string str2 = matches[i].Groups[1].Value;
                                    string str3 = matchesDate[i].Groups[1].Value;
                                    //string str4 = matchesAuthor2[i].Groups[1].Value;
                                    listBox1.Items.Add(str2 + " DATE: " + str3);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < matchesAuthor.Count; i++)
                                {
                                    string str2 = matches[i].Groups[1].Value;
                                    string str4 = matchesAuthor[i].Groups[2].Value;
                                    listBox1.Items.Add(str2 + " by " + str4);
                                }
                            }
                       
                            pages--;    
                        }
                        MatchCollection mathes = regexLink.Matches(line);
                        if(mathes.Count > 0)
                        {
                            foreach(Match match in mathes)
                            {
                                string str2 = match.Groups[1].Value;
                                lines[ind] = str2;
                                ind++;
                            }
                        }
                        
                    }
                    if(pages > 0)
                    {
                        count++;
                        goto Go;
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
