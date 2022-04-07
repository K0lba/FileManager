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
    public partial class Form2 : Form
    {
        private string _path;
        private string _name;
        private ListBox _list;
        private ListBox _list2;
        private int _type;

        public Form2(string path, string text, ListBox listbox1, ListBox listbox2,int type)
        {
            InitializeComponent();
            _path = path;
            _name = text;
            _list = listbox1;
            _list2 = listbox2;
            _type = type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_name +"\\"+textBox1.Text + Path.GetExtension(_path));
            if (File.Exists(_path))
            {
                File.Move(_path,_name +"\\"+textBox1.Text + Path.GetExtension(_path));
                if(_type == 1)
                {
                    _list.Items.RemoveAt(_list.SelectedIndex);
                    _list.Items.Add(textBox1.Text + Path.GetExtension(_path));
                }else
                {
                    _list2.Items.RemoveAt(_list2.SelectedIndex);
                    _list2.Items.Add(textBox1.Text + Path.GetExtension(_path));
                }
            }
            else if (Directory.Exists(_path))
            {
                Directory.Move(_path, _name + "\\" + textBox1.Text);
                if (_type == 1)
                {
                    _list.Items.RemoveAt(_list.SelectedIndex);
                    _list.Items.Add(textBox1.Text);
                }
                else
                {
                    _list2.Items.RemoveAt(_list2.SelectedIndex);
                    _list2.Items.Add(textBox1.Text);
                }
            }
            
            
            
            Close();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
