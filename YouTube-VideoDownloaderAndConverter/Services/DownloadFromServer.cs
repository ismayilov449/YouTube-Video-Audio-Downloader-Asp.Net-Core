using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace YouTube_VideoDownloaderAndConverter.Services
{
    public class DownloadFromServer : PageModel
    {
        private readonly IFileProvider _fileProvider;

        public DownloadFromServer( IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IDirectoryContents PhysicalFiles { get; private set; }

        public async Task OnGetAsync()
        {
            PhysicalFiles = _fileProvider.GetDirectoryContents(string.Empty);
        }
         

        public IActionResult OnGetDownloadPhysical(string fileName)
        {
            var downloadFile = _fileProvider.GetFileInfo(fileName);

            return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, fileName);
        }
    }
}
