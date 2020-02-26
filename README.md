# YouTube-Video-Audio-Downloader-Asp.Net-Core

Used package - VideoLibrary(3.0.0)

Upload file to server & Download file from server (Azure App Service)

•Download Video :

            var youTube = YouTube.Default; // starting point for YouTube actions

            var video = youTube.GetVideo(templink); // gets a Video object with info about the video

            byte[] current = await video.GetBytesAsync();
            

•Upload file to server (UploadToServerModel.cs) :
      
            UploadToServerModel uploadToServerModel = new UploadToServerModel(configuration);

            await uploadToServerModel.OnPostUploadAsync(current,video.FullName);
            

•Download from server (DownloadFromServer.cs) :
      
             Downloader downloader = new Downloader(fileProvider, configuration);
            
             await downloader.DownloadFile(DetailsViewModelMain.Link, DetailsViewModelMain.FilePath,files,fileProvider,configuration);
             

For more look at repo..



