using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;
using System.Collections;

namespace FileManager1
{
    public partial class BookSite : Form
    {
        int pages;
        string[] lines;
        List<Book> list;
        
        [System.ComponentModel.Browsable(true)]
        public BookSite()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            pages = Convert.ToInt32(textBox1.Text);
            int ind = 0;
            int count = 1;
            lines = new string[pages];
            list = new List<Book>();
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

            while (pages > 0)
            {
            client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
            var responseStream = new GZipStream(client.OpenRead("https://www.amazon.com/s?k=" + comboBox1.Text + "&i=stripbooks-intl-ship&page=" + count), CompressionMode.Decompress);
            var reader1 = new StreamReader(responseStream);

            #region Regex
            Regex regexName = new Regex("<span class=\"a-size-medium a-color-base a-text-normal\">(.*?)</span>");
            Regex regexDate = new Regex("<span class=\"a-size-base a-color-secondary a-text-normal\">(.*?)</span>");
            Regex regexAuthor = new Regex("<a class=\"a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style\" href=\"(.*?)\">(.*?)</a>");;
            Regex regexLink = new Regex("<a class=\"a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal\" href=\"(.*?)>");
            Regex regexPrice1 = new Regex("<span class=\"a-price-whole\">(.*?)<span class=\"a-price-decimal\">(.*?)</span>");
            Regex regexPrice2 = new Regex("<span class=\"a-price-fraction\">(.*?)</span>");
            Regex regexRate = new Regex("span class=\"a-icon-alt\">(.*?)</span>");
            #endregion

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
                        Book book = new Book();
                        MatchCollection matches = regexName.Matches(line);
                        if(matches.Count > 0)
                        {
                            MatchCollection mathes = regexLink.Matches(line);
                            MatchCollection matchesDate = regexDate.Matches(line);
                            MatchCollection matchesAuthor = regexAuthor.Matches(line);
                            MatchCollection  matchesPrice1 = regexPrice1.Matches(line);
                            MatchCollection matchesPrice2 = regexPrice2.Matches(line);
                            MatchCollection matchRate = regexRate.Matches(line);

                            if (matches.Count > 0 && matchesDate.Count > 0 && matchesAuthor.Count > 0 && matchesPrice1.Count > 0 && mathes.Count > 0 && matchRate.Count > 0)
                            {
                                for (int i = 0; i < matchesDate.Count; i++)
                                {
                                    book.Author = matchesAuthor[i].Groups[2].Value;
                                    book.Price = matchesPrice1[i].Groups[1].Value + matchesPrice1[i].Groups[2].Value + matchesPrice2[i].Groups[1].Value;
                                    book.Name = matches[i].Groups[1].Value;
                                    book.Rate = matchRate[i].Groups[1].Value.Split(" ")[0];
                                    book.Date = matchesDate[i].Groups[1].Value;
                                    book.Link = mathes[i].Groups[1].Value;
                                    ListViewItem listViewItem = new ListViewItem(new string[] {book.Name ,book.Author ,book.Date,book.Rate,book.Price });
                                    listView1.Items.Add(listViewItem);
                                    list.Add(book);                                 
                                }
                            }
                       
                            pages--;    
                        }
                        
                        
                    }
                    count++;
                    
                }
            }
            }
            

        }


        

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lines != null)
            {
                ListView.SelectedIndexCollection sel = listView1.SelectedIndices;
                string nm = "";
                string link = "";
                if (sel.Count == 1)
                {
                    ListViewItem selItem = listView1.Items[sel[0]];
                    nm = selItem.SubItems[0].Text;
                }
                foreach(var item in list)
                {
                    if (item.Name == nm)
                    {
                        link = item.Link;
                    }
                }
                Process.Start(new ProcessStartInfo("https://www.amazon.com/" + link) { UseShellExecute = true });
            }
        }


        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        class Book
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Date { get; set; }
            public string Author { get; set; }
            public string Price { get; set; }

            public string Rate { get; set; }
        }
    }
}
