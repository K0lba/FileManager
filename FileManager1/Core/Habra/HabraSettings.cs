using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1.Core.Habra
{
    internal class HabraSettings : IParsesSettings
    {
        public string BaseUrl { get; set; } = "https://habrahabr.ru";
        public string Prefix { get; set; } = "page";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
