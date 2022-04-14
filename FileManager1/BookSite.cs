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

            string g = "Python docodcko,Pythone ek";
            Regex regex = new Regex(@"Python(\w*)");
            MatchCollection matches = regex.Matches(g);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    listBox1.Items.Add(match.Value);
            }
            WebClient client = new WebClient();

            using (Stream stream = client.OpenRead("https://docs.microsoft.com/ru-ru/dotnet/api/system.net.webclient.openread?view=netframework-4.8"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line.Normalize());
                    }
                }
            }

        }

    }
}
