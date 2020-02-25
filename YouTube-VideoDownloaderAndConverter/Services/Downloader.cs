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

        public DetailsViewModel SearchFile(string link)
        {
            link = link.Replace("watch?", "");
            link = link.Replace("=", "/");


            
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video = youTube.GetVideo(link); // gets a Video object with info about the video


            var details = new DetailsViewModel()
            {
                FullName = video.FullName,

                Resolution = video.Resolution
            };
              if (FileType.Mp4.ToString() == video.Format.ToString())
            {
                details.Format = FileType.Mp4;
            }
            else
            {
                details.Format = FileType.Mp3;
            }

            return  details;
        }


        public void DownloadFile(string link)
        {
            //www.youtube.com / watch ? v = xxxx

            //www.youtube.com / v / xxxx

            link = link.Replace("watch?", "");
            link = link.Replace("=", "/");

             
                var youTube = YouTube.Default; // starting point for YouTube actions
                var video = youTube.GetVideo(link); // gets a Video object with info about the video
                File.WriteAllBytes(@"C:\Users\Rufat\source\repos\YouTube-VideoDownloaderAndConverter\YouTube-VideoDownloaderAndConverter\Downloads\" + video.FullName, video.GetBytes());
              

        }

 
    }


}

