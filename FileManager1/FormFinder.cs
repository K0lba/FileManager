﻿using System;
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
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                DirectoryInfo[] DIRS1 = directoryInfo.GetDirectories();
                Regex regexText = new Regex(textBox1.Text);
            
                foreach (DirectoryInfo currentdir in DIRS1)
                {
                    MatchCollection matchCollection = regexText.Matches(currentdir.Name);
                    if(matchCollection.Count > 0)
                    {
                        foreach (Match match in matchCollection)
                        {
                            listBox1.Items.Add(match);
                        }
                    }
                
                }

            }
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
