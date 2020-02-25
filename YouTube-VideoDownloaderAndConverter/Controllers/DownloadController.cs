using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouTube_VideoDownloaderAndConverter.Models;
using YouTube_VideoDownloaderAndConverter.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YouTube_VideoDownloaderAndConverter.Controllers
{
    public class DownloadController : Controller
    {
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

            Downloader downloader = new Downloader();
            var details = downloader.SearchFile(searchViewModel.Link);

            return RedirectToAction("DownloadFile", new { link = details.Link });
        }


        [HttpGet]
        public async Task<IActionResult> DownloadFile(Uri link)
        {


            Downloader downloader = new Downloader();
            var details = downloader.SearchFile(link);

            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(DetailsViewModel detailsViewModel, List<IFormFile> files)
        {

        
             Downloader downloader = new Downloader();
            await downloader.DownloadFile(detailsViewModel.Link, detailsViewModel.FilePath,files);

            
          

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.


            return RedirectToAction("SearchFile");
        }
    }
}
