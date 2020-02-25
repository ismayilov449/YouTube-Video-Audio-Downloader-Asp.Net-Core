using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YouTube_VideoDownloaderAndConverter.Services
{
    public class DownloadFromServer
    {
        public DownloadFromServer()
        {
            string currDir = Directory.GetCurrentDirectory(); 
            WebClient request = new WebClient();

            request.Credentials = new NetworkCredential("isma_ml47@itstep.edu.az", "Ismayilov1");

            byte[] fileData = request.DownloadData(currDir + "/mymy.txt");

            FileStream file = File.Create(currDir + "\\mymy.txt");

            file.Write(fileData, 0, fileData.Length);

            file.Close();

        }
    }
}
