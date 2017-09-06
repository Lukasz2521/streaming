using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace streaming_inż.Models
{
    public class FileUpload
    {   
        [Required(ErrorMessage ="Podaj nazwę utworu")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wybierz avatar")]
        public HttpPostedFileBase Avatar { get; set; }
        
        [Required(ErrorMessage = "Dodaj utwór")]
        public HttpPostedFileBase Song { get; set; }
    }

    public class ExtractFile: BaseSong
    {
        public int cutFrom { get; set; }

        public int cutTo { get; set; }
    }

    public class UserSong: BaseSong
    {
        public string UserName { get; set; }

        public string Title { get; set; }

        public string UploadTime { get; set; }

        public string avatarPath { get; set; }
    }
}