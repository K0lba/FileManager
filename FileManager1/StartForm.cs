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
            textBox1.Text = settings.GetLogin();
            textBox2.Text = settings.GetPassword();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
            
            settings.SetLogin(textBox1.Text);
            settings.SetPassword(textBox2.Text);
            settings.Save();


           /* Close();
            th = new Thread(Open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/

        }
        private void Open(object obj)
        {
            Application.Run(new Form1());
        }
    }
}
