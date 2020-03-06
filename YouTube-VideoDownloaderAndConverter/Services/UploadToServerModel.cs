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

            _targetFilePath = Directory.GetCurrentDirectory() + @"\wwwroot\";
        }

        [BindProperty]
        public UploadToServer FileUpload { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
        }

        public async Task OnPostUploadAsync(byte[] currentFile,string fileName)
        {

            var trustedFileNameForFileStorage = fileName;
            var filePath = Path.Combine(
                _targetFilePath, trustedFileNameForFileStorage);
 
            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(currentFile);

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



