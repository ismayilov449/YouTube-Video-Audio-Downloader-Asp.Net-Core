using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VideoLibrary;
using YouTube_VideoDownloaderAndConverter.Models;
using YouTube_VideoDownloaderAndConverter.Models.Enums;

namespace YouTube_VideoDownloaderAndConverter.Services
{
    public class Downloader
    {

        public  DetailsViewModel SearchFile(Uri link)
        {
            string templink = link.OriginalString;

            templink = templink.Replace("watch?", "");
            templink = templink.Replace("=", "/");


            
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video =  youTube.GetVideo(templink); // gets a Video object with info about the video

            var currentPath = Directory.GetCurrentDirectory();
             

            DetailsViewModel details = new DetailsViewModel()
            {
                FullName = video.FullName,
                Link = new Uri(templink, UriKind.Absolute),
                Resolution = video.Resolution,
                FilePath = currentPath + @"\Downloads\" + video.FullName

            };
              if (FileType.Mp4.ToString() == video.Format.ToString())
            {
                details.Format = FileType.Mp4;
            }
            else
            {
                details.Format = FileType.Mp3;
            }


            return details;
        }


        public async Task DownloadFile(Uri link,string FilePath, List<IFormFile> files)
        { 

             
            string templink = link.OriginalString;

            templink = templink.Replace("watch?", "");
            templink = templink.Replace("=", "/");

            var youTube = YouTube.Default; // starting point for YouTube actions
             
            var video = youTube.GetVideo(templink); // gets a Video object with info about the video
         
            var currentPath = Directory.GetCurrentDirectory();
           
            byte[] current = await video.GetBytesAsync();

            await File.WriteAllBytesAsync(currentPath + @"\Downloads\" + video.FullName,current);

            UploadToServer uploadToServer = new UploadToServer(currentPath + @"\Downloads\" + video.FullName);

            long size = files.Sum(f => f.Length);


            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = FilePath; //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = new MemoryStream(current))
                    {
                        
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

        }

        public string FormatFileSize(byte[] bytes)
        {
          

            if (bytes.Count() >= 1000000000)
            {
                return (bytes.Count() / 1000000000).ToString().Substring(0,3) + " GB";
            }

            if (bytes.Count() >= 1000000)
            {
                return (bytes.Count() / 1000000).ToString().Substring(0, 3) + " MB";
            }

            return (bytes.Count() / 1000).ToString().Substring(0, 3) + " KB";
        }

    }
     
}

