using System.Diagnostics;

namespace FileManager1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            DirectoryInfo DIR = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] DIRS = DIR.GetDirectories();

            foreach(DirectoryInfo currentdir in DIRS)
            {
                listBox1.Items.Add(currentdir.Name);
            }

            FileInfo[] FILES = DIR.GetFiles();

            foreach(FileInfo file in FILES)
            {
                listBox1.Items.Add(file.Name);
                
            }

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            textBox1.Text= textBox1.Text+ "\\" +listBox1.SelectedItem.ToString();
             
            if (Path.GetExtension(textBox1.Text) == "")
            {
                
                listBox1.Items.Clear();

                DirectoryInfo DIR = new DirectoryInfo(textBox1.Text);
                DirectoryInfo[] DIRS = DIR.GetDirectories();

                foreach (DirectoryInfo currentdir in DIRS)
                {
                    listBox1.Items.Add(currentdir.Name);
                }

                FileInfo[] FILES = DIR.GetFiles();

                foreach (FileInfo file in FILES)
                {
                    listBox1.Items.Add(file.Name);

                }
            }
            else
            {
                Process.Start(textBox1.Text) ;
            }
            

        }

        
    }
}