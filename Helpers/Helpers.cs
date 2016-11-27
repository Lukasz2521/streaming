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
            var absolutePath = GetHomePath();
            string filePath = absolutePath + @"/Content/Images/" + imageName;
             
            return filePath;
        }

        public static string GetHomePath()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
        }
    }
}