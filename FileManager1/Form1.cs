using System.Diagnostics;
using System.Text;
using Ionic.Zip;


namespace FileManager1
{
    public partial class Form1 : Form
    {
        private string DelName;
        public Form1()
        {
            InitializeComponent();
            Init();


        }
        private void Init()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            textBox1.Text = drives[0].Name;
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
            textBox2.Text = drives[1].Name;
            DirectoryInfo DIR1 = new DirectoryInfo(textBox2.Text);
            DirectoryInfo[] DIRS1 = DIR1.GetDirectories();

            foreach (DirectoryInfo currentdir in DIRS1)
            {
                listBox2.Items.Add(currentdir.Name);
            }

            FileInfo[] FILES1 = DIR.GetFiles();

            foreach (FileInfo file in FILES1)
            {
                listBox2.Items.Add(file.Name);

            }

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
            if (File.Exists(textBox1.Text))
            {
                
                Process.Start(new ProcessStartInfo(textBox1.Text) { UseShellExecute = true});
            }
            else
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            DirectoryInfo DIR = new DirectoryInfo(textBox2.Text);
            DirectoryInfo[] DIRS = DIR.GetDirectories();

            foreach (DirectoryInfo currentdir in DIRS)
            {
                listBox2.Items.Add(currentdir.Name);
            }

            FileInfo[] FILES = DIR.GetFiles();

            foreach (FileInfo file in FILES)
            {
                listBox2.Items.Add(file.Name);

            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = textBox2.Text + "\\" + listBox2.SelectedItem.ToString();
            if (File.Exists(textBox2.Text))
            {

                Process.Start(new ProcessStartInfo(textBox2.Text) { UseShellExecute = true });
            }
            else
            {
                listBox2.Items.Clear();

                DirectoryInfo DIR = new DirectoryInfo(textBox2.Text);

                DirectoryInfo[] DIRS = DIR.GetDirectories();

                foreach (DirectoryInfo currentdir in DIRS)
                {
                    listBox2.Items.Add(currentdir.Name);
                }

                FileInfo[] FILES = DIR.GetFiles();

                foreach (FileInfo file in FILES)
                {
                    listBox2.Items.Add(file.Name);

                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                File.Delete(DelName);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);


            }
            else if (listBox1.SelectedItem != null)
            { 
                File.Delete(DelName);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                string del= textBox1.Text + "\\" + listBox1.SelectedItem.ToString();
                DelName = del;
            }
            else if(listBox2.SelectedItem != null)
            {
                string del = textBox2.Text + "\\" + listBox2.SelectedItem.ToString();
                DelName = del;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {

            }
            else if (listBox1.SelectedItem != null)
            {

                string delname = DelName;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (ZipFile zip = new ZipFile(Encoding.UTF8))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                    zip.AddFile(delname); // ������ � ����� ��������� ����
                    string zipfile = delname.Remove(delname.Length - 4); //�������� ���������� �����
                    zip.Save(zipfile + ".zip");
                }
                listBox1.Items.Add(DelName);
            }

        }
    }
}