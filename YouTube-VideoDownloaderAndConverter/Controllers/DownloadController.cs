using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using YouTube_VideoDownloaderAndConverter.Models;
using YouTube_VideoDownloaderAndConverter.Models.Enums;
using YouTube_VideoDownloaderAndConverter.Services;


namespace YouTube_VideoDownloaderAndConverter.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IFileProvider fileProvider;
        private readonly IConfiguration configuration;
        public static DetailsViewModel DetailsViewModelMain = new DetailsViewModel();

        public DownloadController(IFileProvider _fileProvider,IConfiguration _configuration)
        {
            configuration = _configuration;
            fileProvider = _fileProvider;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> SearchFile()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchFile(SearchViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                return View();
            }

            Downloader downloader = new Downloader(fileProvider,configuration);
            var details = downloader.SearchFile(searchViewModel.Link);
            DetailsViewModelMain = details;

            return RedirectToAction("DownloadFile");
        }

 
        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {
             
             Downloader downloader = new Downloader(fileProvider, configuration);
             await downloader.DownloadFile(DetailsViewModelMain.Link,configuration);
             
  
            return View(DetailsViewModelMain);
        }


        [HttpGet]
        public async Task<IActionResult> DownloadPhysical(DetailsViewModel detailsViewModel)
        {
             
            var downloadFile = fileProvider.GetFileInfo(detailsViewModel.FullName);
            string fileExtension;

            if(FileType.Mp3.ToString() == detailsViewModel.Format.ToString())
            {
                string audioFile = Path.GetFileNameWithoutExtension(detailsViewModel.FullName);
                fileExtension = audioFile + ".mp3";
                return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, fileExtension);
            }
            else
            {
                return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, detailsViewModel.FullName);
            }
             

        }
    }
}
