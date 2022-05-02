using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace FileManager1
{
    public class DownLoder
    {
        public CancellationTokenSource cancelTokenSource;
        public DownLoder()
        {

        }
        public int DownloadFile(string remoteFilename,
                               string localFilename)
        {

            int bytesProcessed = 0;

            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            try
            {
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {

                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();

                        localStream = File.Create(localFilename);

                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        cancelTokenSource = new CancellationTokenSource();
                        var token = cancelTokenSource.Token;
                        do
                        {
                            
                            if (token.IsCancellationRequested)
                            {
                                MessageBox.Show("Canceled operation");
                                return 0;
                                
                            }
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            localStream.Write(buffer, 0, bytesRead);

                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }
            return bytesProcessed;
        }
    }
}
