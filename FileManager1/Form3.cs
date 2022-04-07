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
    public partial class Form3 : Form
    {
        private string _path;
        private string _name;
        private ListBox _list;

        public Form3(string path,string name,ListBox list)
        {
            InitializeComponent();
            _path = path;
            _name = name;
            _list = list;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(File.Exists(_path))
                File.Move(_path, textBox1.Text+"\\"+_name);
            if(Directory.Exists(_path))
                Directory.Move(_path, textBox1.Text + "\\" + _name);
            _list.Items.RemoveAt(_list.SelectedIndex);
            
            
            Close();
        }
    }
}
