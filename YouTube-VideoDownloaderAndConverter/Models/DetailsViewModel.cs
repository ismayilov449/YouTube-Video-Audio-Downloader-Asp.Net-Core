using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTube_VideoDownloaderAndConverter.Models.Enums;

namespace YouTube_VideoDownloaderAndConverter.Models
{
    public class DetailsViewModel
    {
        public string FullName { get; set; }

        public FileType Format { get; set; }

        public int Resolution { get; set; }

    }
}
