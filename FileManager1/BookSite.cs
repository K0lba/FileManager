using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;

namespace FileManager1
{
    public partial class BookSite : Form
    {
        string progLang;
        int pages;
        string[] lines;
        [System.ComponentModel.Browsable(true)]
        public System.Windows.Forms.AutoCompleteMode AutoCompleteMode { get; set; }
        public BookSite()
        {
            InitializeComponent();
            //listView1.Items.Add("Dont touch");
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            progLang = comboBox1.Text;
            pages = Convert.ToInt32(textBox1.Text);
            int ind = 0;
            int count = 1;
            lines = new string[pages];

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

            Go:
            client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            var responseStream = new GZipStream(client.OpenRead("https://www.amazon.com/s?k=" + comboBox1.Text + "&i=stripbooks-intl-ship&page=" + count), CompressionMode.Decompress);
            var reader1 = new StreamReader(responseStream);
            //var textResponse = reader1.ReadToEnd();
            //StreamWriter file = new StreamWriter("C:\\Новая папка\\g.txt");
            //file.Write(textResponse);


            string str = client.DownloadString("https://www.amazon.com/");






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
            //Go:
            using (Stream stream = responseStream)
            {
                
                using (StreamReader reader = reader1)
                {
                    string line = "";
                    while ((line = reader1.ReadLine()) != null )
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
                                    ListViewItem listViewItem = new ListViewItem(new string[] {matches[i].Groups[1].Value,matchesDate[i].Groups[1].Value, matchesAuthor[i].Groups[2].Value});
                                    listView1.Items.Add(listViewItem);
                                    //string str2 = ;
                                    //string str3 = ;
                                    //string str4 = ;
                                    //listView1.Items.Add(str2 + " by " + str4 + " DATE: " + str3);
                                }
                            }
                            /*else if(matchesDate.Count > 0)
                            {
                                //Regex newAuthor = new Regex("<span class=\"a-size-base\">(.*?)</span>");
                                //MatchCollection matchesAuthor2 = regexAuthor.Matches(line);
                                for (int i = 0; i < matchesDate.Count; i++)
                                {
                                    string str2 = matches[i].Groups[1].Value;
                                    string str3 = matchesDate[i].Groups[1].Value;
                                    //string str4 = matchesAuthor2[i].Groups[1].Value;
                                    listView1.Items.Add(str2 + " DATE: " + str3);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < matchesAuthor.Count; i++)
                                {
                                    string str2 = matches[i].Groups[1].Value;
                                    string str4 = matchesAuthor[i].Groups[2].Value;
                                    listView1.Items.Add(str2 + " by " + str4);
                                }
                            }*/
                       
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
                Process.Start(new ProcessStartInfo("https://www.amazon.com/"+lines[listView1.Items.Count]) { UseShellExecute = true }); 
            }
              
            if(listView1.SelectedItems.ToString() == "Dont touch")
                Process.Start(new ProcessStartInfo(@"https://www.youtube.com/watch?v=DLzxrzFCyOs&ab_channel=AllKindsOfStuff") { UseShellExecute = true });
            //System.Diagnostics.Process.Start(line[1]);
            
                
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lines != null)
            {
                //var line = lines[listBox1.SelectedIndex].Split('"');
                Process.Start(new ProcessStartInfo("https://www.amazon.com/" + lines[listView1.Columns[0].Index]) { UseShellExecute = true });
            }

            if (listView1.SelectedItems.ToString() == "Dont touch")
                Process.Start(new ProcessStartInfo(@"https://www.youtube.com/watch?v=DLzxrzFCyOs&ab_channel=AllKindsOfStuff") { UseShellExecute = true });
            //System.Diagnostics.Process.Start(line[1]);
        }
    }
}
