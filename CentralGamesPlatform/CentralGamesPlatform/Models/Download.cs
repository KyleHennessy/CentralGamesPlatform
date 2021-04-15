using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class Download
    {
        [BindNever]
        public int DownloadId { get; set; }
        public int GameId { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileFormat { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public double VersionNumber { get; set; }
        [NotMapped]
        public FileUpload fileObject { get; set; }
    }
}
