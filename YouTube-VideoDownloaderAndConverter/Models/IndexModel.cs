using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace YouTube_VideoDownloaderAndConverter.Models
{
    public class IndexModel : PageModel
    {
        private readonly IFileProvider _fileProvider;

        public IndexModel(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IDirectoryContents PhysicalFiles { get; private set; }

        public async Task OnGetAsync()
        {
            PhysicalFiles = _fileProvider.GetDirectoryContents(string.Empty);
        }
         
        public async Task<IActionResult> OnGetDownloadPhysical(string fileName)
        {
            var downloadFile = _fileProvider.GetFileInfo(@"C:\Users\Rufat\source\repos\YouTube-VideoDownloaderAndConverter\YouTube-VideoDownloaderAndConverter\Downloads\");

            await OnGetAsync();

            return PhysicalFile(@"C:\Users\Rufat\source\repos\YouTube-VideoDownloaderAndConverter\YouTube-VideoDownloaderAndConverter\Downloads\myfile.txt", MediaTypeNames.Application.Octet, fileName);
        }
    }
}
