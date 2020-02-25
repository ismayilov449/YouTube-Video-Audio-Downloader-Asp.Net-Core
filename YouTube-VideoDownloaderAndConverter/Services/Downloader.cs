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

        public DetailsViewModel SearchFile(Uri link)
        {
            string templink = link.OriginalString;

            templink = templink.Replace("watch?", "");
            templink = templink.Replace("=", "/");


            
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video = youTube.GetVideo(templink); // gets a Video object with info about the video



            var details = new DetailsViewModel()
            {
                FullName = video.FullName,
                Link = new Uri(templink, UriKind.Absolute),
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


        public void DownloadFile(Uri link,string filePath)
        { 

             
            string templink = link.OriginalString;

            templink = templink.Replace("watch?", "");
            templink = templink.Replace("=", "/");

            var youTube = YouTube.Default; // starting point for YouTube actions

            

                var video = youTube.GetVideo(templink); // gets a Video object with info about the video

           

            var currentPath = Directory.GetCurrentDirectory();

                File.WriteAllBytes(currentPath + @"\Downloads\" + video.FullName, video.GetBytes());
               
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

    }
     
}

