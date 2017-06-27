using streaming_inż.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace streaming_inż.Controllers
{
    public class HomeController : Controller
    {
        private SongRepository song = new SongRepository();

        public ActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        public JsonResult Search(string keyWord)
        {
            var matchedWords = song.findByKeyword(keyWord, User.Identity.GetUserId());
              
            return Json(matchedWords);
        }

        public ActionResult SearchResult(string searchedWord)
        {
            var matchedSongs = song.getByKeyword(searchedWord, User.Identity.GetUserId());
            ViewBag.searchedWord = searchedWord;

            return View("SearchResult", matchedSongs);
        }

        public ActionResult UnsignedUser()
        {
            return View(); // add view for unsigned user
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}