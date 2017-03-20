using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace streaming_inż.Helpers
{
    public static class Helpers
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

        public static string getTimeStamp(DateTime uploadDate)
        {
            DateTime now = DateTime.Now;

            return ((now.Date - uploadDate.Date).TotalDays).ToString();
        }

        public static string getAvatarPath(string userId, int songId)
        {
            string avatarPath = Path.Combine(HttpContext.Current.Server.MapPath("/avatars"), String.Concat(userId, "_", songId.ToString(), ".jpg"));
            return avatarPath;
        }

        public static string getSongPath(string userId, int songId)
        {
            string songPath = Path.Combine(HttpContext.Current.Server.MapPath("/songs"), String.Concat(userId, "_", songId.ToString(), ".mp3"));
            return songPath;
        }
    }
}