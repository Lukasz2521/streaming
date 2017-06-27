using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace streaming_inż
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Register-path",
                url: "Register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login-path",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "User-profile",
                url:  "user/{userName}",
                defaults: new { controller = "Profile", action = "Index" }
            );

            routes.MapRoute(
                name: "SongUpload",
                url: "upload",
                defaults: new { controller = "Profile", action = "UploadSong" }
            );

            routes.MapRoute(
                name: "Search",
                url: "search/{searchedWord}",
                defaults: new { controller = "Home", action = "SearchResult" }
            );

            routes.MapRoute(
                name: "Favorite",
                url: "favorite",
                defaults: new { controller = "Profile", action = "GetFavoriteSongs" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
