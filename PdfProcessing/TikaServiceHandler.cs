using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing
{
    public class TikaServiceHandler
    {
        //Make port binding to handle by docker, not hard-coded
        private readonly string TIKKA_CONNECTION_URL = "http://localhost:9998/tika";

        public string ReadPdfFile(string path)
        {
            WebClient wc = new WebClient();

            var tikaUrl = new Uri(TIKKA_CONNECTION_URL);

            
            var response = wc.UploadData(tikaUrl, "PUT", File.ReadAllBytes(path));

            if (response == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(response);
        }

        public string ReadPdfFile(byte[] data)
        {
            WebClient wc = new WebClient();

            var tikaUrl = new Uri(TIKKA_CONNECTION_URL);


            var response = wc.UploadData(tikaUrl, "PUT", data);

            if (response == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(response);
        }
    }
}
