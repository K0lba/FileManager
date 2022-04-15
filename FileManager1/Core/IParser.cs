using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1.Core
{
    internal interface IParser<T> where T : class
    {
        T Parser (IHtmlDocument document);
    }
}
