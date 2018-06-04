using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessing
{
    public class TikaServiceHandler
    {
        private readonly string TIKKA_CONNECTION_URL = "http://localhost:9998/tika";

        public async Task<string> ReadPdfFile(string path)
        {
            WebClient wc = new WebClient();

            var tikaUrl = new Uri(TIKKA_CONNECTION_URL);
            wc.Headers.Add("X-Tika-PDFextractInlineImages", "true");

            
            var response = await wc.UploadDataTaskAsync(tikaUrl, "PUT", File.ReadAllBytes(path));

            if (response == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(response);
        }
    }
}
