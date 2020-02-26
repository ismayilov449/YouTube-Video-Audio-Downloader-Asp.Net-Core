﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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
        private readonly IFileProvider fileProvider;
        private readonly IConfiguration configuration;

        public Downloader(IFileProvider _fileProvider, IConfiguration _configuration)
        {
            fileProvider = _fileProvider;
            configuration = _configuration;
        }

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
                FilePath = currentPath + @"\wwwroot\Downloads\" + video.FullName

            };
            IndexModel indexModel = new IndexModel(fileProvider);
            indexModel.OnGetDownloadPhysical(details.FilePath);

            details.IndexModel = indexModel;

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


        public async Task DownloadFile(Uri link,string FilePath, IFormFile files,IFileProvider fileProvider,IConfiguration configuration)
        { 

             
            string templink = link.OriginalString;

            templink = templink.Replace("watch?", "");
            templink = templink.Replace("=", "/");





            var youTube = YouTube.Default; // starting point for YouTube actions

            var video = youTube.GetVideo(templink); // gets a Video object with info about the video

            var currentPath = Directory.GetCurrentDirectory();

            byte[] current = await video.GetBytesAsync();

            UploadToServerModel uploadToServerModel = new UploadToServerModel(configuration);

            await uploadToServerModel.OnPostUploadAsync(current,video.FullName);

            //IndexModel indexModel = new IndexModel(fileProvider);

            //indexModel.OnGetDownloadPhysical(uploadToServerModel.FileUpload.FormFile.FileName.ToString());
            


            //await File.WriteAllBytesAsync(currentPath + @"\Downloads\" + video.FullName,current);

            //UploadToServerModel uploadToServer = new UploadToServerModel();

            //await uploadToServer.UploadFile(files);


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

