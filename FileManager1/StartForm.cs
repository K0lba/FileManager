using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager1
{
    public partial class StartForm : Form
    {
        Thread th;
        Authorization settings;
        public StartForm()
        {
            InitializeComponent();
            settings = Authorization.GetSettings();
            textBox1.Text = settings.Login;
            textBox2.Text = settings.Password;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1(settings);
            frm.Show();
            
            settings.Login = textBox1.Text;
            settings.Password = textBox2.Text;
            settings.Save();
        }
    }
}
