using System.Diagnostics;
using System.Text;
using Ionic.Zip;


namespace FileManager1
{
    public partial class Form1 : Form
    {
        private string DelName;
        private string DelName2;
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
                if (File.Exists(DelName2))
                    File.Delete(DelName2);
                else Directory.Delete(DelName2);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);


            }
            else if (listBox1.SelectedItem != null)
            {
                if (File.Exists(DelName))
                    File.Delete(DelName);
                else Directory.Delete(DelName);
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
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                
                string delname2 = DelName2;
                string res;
                string zipfile2;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (ZipFile zip = new ZipFile(Encoding.UTF8))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                    if (delname2 is File)
                    {
                        zip.AddFile(delname2);
                        zipfile2 = delname2.Remove(delname2.Length - 4); // Кладем в архив одиночный файл
                        res = listBox2.SelectedItem.ToString().Remove(listBox2.SelectedItem.ToString().Length - 4) + ".zip";
                    }

                    else
                    {
                        zip.AddDirectory(delname2);
                        zipfile2 = delname2;
                        res = listBox2.SelectedItem.ToString() + ".zip";
                    }
                    
                    zip.Save(zipfile2 + ".zip");
                }
                listBox2.Items.Add(res);

            }
            else if (listBox1.SelectedItem != null)
            {

                string delname = DelName;
                string res;
                string zipfile;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (ZipFile zip = new ZipFile(Encoding.UTF8))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                    if(File.Exists(delname))
                    {
                        zip.AddFile(delname);
                        zipfile = delname.Remove(delname.Length - 4); // Кладем в архив одиночный файл
                        res = listBox1.SelectedItem.ToString().Remove(listBox1.SelectedItem.ToString().Length - 4) + ".zip";
                    }

                    else
                    {
                        zip.AddDirectory(delname);
                        zipfile = delname;
                        res = listBox1.SelectedItem.ToString() + ".zip";
                    }

                    //Обрезаем расширение файла
                    zip.Save(zipfile + ".zip");
                }
                listBox1.Items.Add(res);
            }

        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string del2 = textBox2.Text + "\\" + listBox2.SelectedItem.ToString();
                DelName2 = del2;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form;
            if (listBox1.SelectedItem != null)
            {
                form = new Form2(DelName, textBox1.Text,listBox1,listBox2,1) ;
                form.Show();
                
                
            }
            else if (listBox2.SelectedItem != null)
            {
                form = new Form2(DelName2,textBox2.Text,listBox1,listBox2,2);
                form.Show();   
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form;
            if(listBox1.SelectedItem != null)
            {
                form = new Form3(DelName, listBox1.SelectedItem.ToString(),listBox1);
                form.Show();
            }
            else if(listBox2.SelectedItem != null)
            {
                form = new Form3(DelName2, listBox2.SelectedItem.ToString(), listBox2);
                form.Show();
            }
        }



        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            string file = e.Data.GetData(DataFormats.FileDrop).ToString();
            listBox1.Items.Add(file);
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            File.Move(file[0], textBox1.Text + '\\'+ GetName(file[0]));
            listBox1.Items.Add( GetName(file[0]));
        }

        private void listBox1_DragLeave(object sender, EventArgs e)
        {

        }

        private string GetName(string path)
        {
            string name="";
            var strings = path.Split('\\');
            name = strings[strings.Length-1];
            return name;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                if (File.Exists(DelName))
                {
                    File.Copy(DelName, textBox1.Text+"\\"+"copy"+Path.GetExtension(DelName));
                    listBox1.Items.Add("copy" + Path.GetExtension(DelName));
                }
                else
                {
                    
                }
            }
            

        }
    }
}