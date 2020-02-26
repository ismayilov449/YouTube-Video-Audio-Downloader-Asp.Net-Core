using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace YouTube_VideoDownloaderAndConverter.Services
{
    public class UploadToServerModel
    {


        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".txt" , ".mp4" };
        private readonly string _targetFilePath;

        public UploadToServerModel(IConfiguration config)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            // To save physical files to the temporary files folder, use:
            //_targetFilePath = Path.GetTempPath();
        }

        [BindProperty]
        public UploadToServer FileUpload { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
        }

        public async Task OnPostUploadAsync(byte[] currentFile,string fileName)
        {


            // For the file name of the uploaded file stored
            // server-side, use Path.GetRandomFileName to generate a safe
            // random file name.
            var trustedFileNameForFileStorage = fileName;
            var filePath = Path.Combine(
                _targetFilePath, trustedFileNameForFileStorage);

            // **WARNING!**
            // In the following example, the file is saved without
            // scanning the file's contents. In most production
            // scenarios, an anti-virus/anti-malware scanner API
            // is used on the file before making the file available
            // for download or for use by other systems. 
            // For more information, see the topic that accompanies 
            // this sample.

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(currentFile);

                // To work directly with a FormFile, use the following
                // instead:
                //await FileUpload.FormFile.CopyToAsync(fileStream);
            }

        }
    }

    public class UploadToServer 
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }

}



