using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;



namespace FileManager1.Core.Habra
{
    internal class HabraParser : IParser<string[]>
    {
        public string[] Parser(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("p");
                //.Where(items => items.ClassName != null && items.ClassName.Contains("post_title_link"));

            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
           
        }
    }
}
