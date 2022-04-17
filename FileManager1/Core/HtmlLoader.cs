using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1.Core
{
    internal class HtmlLoader
    {

        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParsesSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}/{settings.Prefix}/";
        }
        public async Task<string> GetSourceByPage(int id)
        {
            var currentUrl = url;
                //.Replace("{CurrentId}",id.ToString());
            var response = await client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
