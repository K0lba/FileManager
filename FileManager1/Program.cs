using System.Xml.Serialization;

namespace FileManager1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            Authorization au = new Authorization();
            au.Login = "cum";
            au.Password = "password";
            au.Save();
        }
    }
}