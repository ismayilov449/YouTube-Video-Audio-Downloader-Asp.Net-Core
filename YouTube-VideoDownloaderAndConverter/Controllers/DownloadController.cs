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
using YouTube_VideoDownloaderAndConverter.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            return RedirectToAction("DownloadFile", new { link = details.Link });
        }


        [HttpGet]
        public async Task<IActionResult> DownloadFile(Uri link)
        {
             
            Downloader downloader = new Downloader(fileProvider, configuration);
            var details = downloader.SearchFile(link);

            DetailsViewModelMain = details;

            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(DetailsViewModel detailsViewModel, IFormFile files)
        {
             
             Downloader downloader = new Downloader(fileProvider, configuration);
            await downloader.DownloadFile(DetailsViewModelMain.Link, DetailsViewModelMain.FilePath,files,fileProvider,configuration);
             
  
            return View(DetailsViewModelMain);
        }


        [HttpGet]
        public async Task<IActionResult> DownloadPhysical(string fileName)
        {
             
            var downloadFile = fileProvider.GetFileInfo(fileName);
             

            return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, fileName);

        }
    }
}
