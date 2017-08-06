using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NAudio;
using NAudio.Wave;
using streaming_inż.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using streaming_inż.DAL;
using System.Threading;
using System.Threading.Tasks;

namespace streaming_inż.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private SongRepository song = new SongRepository();
      

        [OutputCache(Duration = 60)]
        public ActionResult Index(string userName)
        {
            var allUserSongsModel = song.getAllUserSongs(User.Identity.GetUserId());
                                         
            return View(allUserSongsModel);
        }


        public ActionResult UserProfile(int userId)
        {
            return View("UserProfile");
        }

        [HttpGet]
        public ActionResult UploadSong()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadSong(FileUpload songUpload)
        {
            string songUploadDate = DateTime.Now.ToString("dd.MM.yyy");

            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId().ToString();
                await song.saveSongAsync(userId, songUpload.Title, songUploadDate.ToString());
                int lastAddedSongId = song.getSongTableCount();
                songUpload.Avatar.SaveAs(Helpers.getAvatarPath(userId, lastAddedSongId));
                songUpload.Song.SaveAs(Helpers.getSongPath(userId, lastAddedSongId));

                return RedirectToAction("Index", "Home");
            }
            else
            { 
                return View(songUpload);
            }                                     
        }
        
        public string extractSong(ExtractFile file)
        {
            song.ExtractSampleFromSong(file);
            return getExtractedSong(file);
        }
        
        public ActionResult GetFavoriteSongs()
        {
            var likedSongs = song.GetFavoriteSongs(User.Identity.GetUserId().ToString());

            return View("_SongContainer", likedSongs);
        }


        public async Task<ActionResult> AddFavoriteSong(string songId)
        {
            var likedSong = new LikedSong()
            {
                UserId = User.Identity.GetUserId(),
                SongId = int.Parse(songId.Split('_')[1])
            };
            await song.AddFavoriteSongAsync(likedSong);
            return Json(new { });
        }

        private string getExtractedSong(ExtractFile file)
        {
            string fullPath = Path.Combine(String.Concat("http://localhost:62316/extract/", file.songId, ".mp3"));
 
            return fullPath;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetWaitingSongs()
        {
            var waitingSongs = song.GetWaitingSongs();

            return View("_SongContainer", waitingSongs);
        }
    }
}