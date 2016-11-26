using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace streaming_inż.Helpers
{
    public class Helpers
    {
        public static string GetImage(string imageName)
        {       
            var absolutePath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;

            string filePath = absolutePath + @"/Content/Images" + imageName;
             
            return filePath;
        }
    }
}