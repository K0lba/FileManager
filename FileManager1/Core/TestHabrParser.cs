using FileManager1.Core.Habra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager1.Core
{
    public partial class TestHabrParser : Form
    {
        ParserWorker<string[]> worker;
        public TestHabrParser()
        {
            InitializeComponent();
            worker = new ParserWorker<string[]>(new HabraParser());
            worker.OnCompleted += Parser_OnCompleted;
            worker.OnNewData += Parser_OnNewData;
        }
        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            listBox1.Items.AddRange(arg2);
        }

        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("All works done!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker.Settings = new HabraSettings((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            worker.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            worker.Abort();
        }
    }
}
