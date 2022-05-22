using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager1
{
    public partial class FormFinder : Form
    {
        string path;
        string expansion;
        public DownLoder downLoder = null;
        List<string> finded = new List<string>();
        public FormFinder(string path, Color color, string font, int size)
        {
            InitializeComponent();
            if (size != null && font != null)
                this.Font = new Font(font, size);
            if (color != null)
                this.BackColor = color;
            this.path = path;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            finded.Clear();
            
            
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    string line = "";
                    Regex regexText = new Regex(textBox1.Text);
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line == null)
                        {
                            continue;
                        }
                        MatchCollection matchCollection = regexText.Matches(line);
                        if (matchCollection.Count > 0)
                        {
                            Parallel.ForEach(matchCollection, match=> { ListBoxAdd(line); });
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            else
            {
            Recursive(path);
                void Recursive(string path)
                {
                    Parallel.ForEach(Directory.GetDirectories(path), curr =>
                    {
                        Regex regexText = new Regex(textBox1.Text);
                        MatchCollection matchCollection = regexText.Matches(new DirectoryInfo(curr).Name);
                        if(matchCollection.Count > 0) { finded.Add(new DirectoryInfo(curr).Name); }
                        Recursive(curr);
                    });
                    Parallel.ForEach(Directory.GetFiles(path), curr =>
                    {
                        Regex regexF = new Regex(textBox1.Text);
                        MatchCollection temp = regexF.Matches(new DirectoryInfo(curr).Name);
                        if (temp.Count > 0) { finded.Add(new DirectoryInfo(curr).Name); }

                    });
                
                }
            }
            string[] finded2 = new string[finded.Count];
            for (var i = 0; i < finded.Count; i++) { finded2[i] = finded[i]; }
            listBox1.Items.AddRange(finded2);
            textBox1.Text = "";

        }
        public void ListBoxAdd(string match)
        {
            this.Invoke(() => listBox1.Items.Add(match));
        }

        private void FormFinder_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Task task = new Task(() =>
            {
                downLoder = new DownLoder();
                expansion = textBox1.Text.Split('/')[textBox1.Text.Split('/').Length - 1];
                int result = downLoder.DownloadFile(textBox1.Text, "C:\\Users\\aruds\\Downloads\\" + expansion);
                MessageBox.Show(result.ToString());
            });
            task.Start();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            downLoder.cancelTokenSource.Cancel();
            Thread.Sleep(5000);
            if (File.Exists("C:\\Users\\aruds\\Downloads\\" + expansion))
            {
                File.Delete("C:\\Users\\aruds\\Downloads\\" + expansion);
            }
        }

    
    }
}
