using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace streaming_inż.Models
{
    public class FileUpload
    {   
        [Required(ErrorMessage ="Please enter track name")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please choose avatar")]
        public HttpPostedFileBase Avatar { get; set; }
        
        [Required(ErrorMessage = "Please choose song")]
        public HttpPostedFileBase Song { get; set; }
    }

    public class ExtractFile
    {
        public string songId { get; set; }
        public int cutFrom { get; set; }
        public int cutTo { get; set; }
    }

    public class UserSongs
    {
        public string SongId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string UploadTime { get; set; }

        public string avatarPath { get; set; }
    }
}