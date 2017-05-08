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


      // GET: Profile
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
        
        public void extractSong(ExtractFile file)
        {
            song.ExtractSampleFromSong(file.cutFrom, file.cutTo, file.songId);
        }

        #region Helpers

        private void SaveFile(int userId, int songId, fileType typeOfFile, HttpPostedFileBase file)
        {
            switch (typeOfFile)
            {
                case fileType.avatar:
                    file.SaveAs(Helpers.getAvatarPath(userId.ToString(), songId));    
                    break;
                case fileType.song:
                    {

                    }
                    break;
            }
        }

        enum fileType
        {
            song,
            avatar
        }

        #endregion
    }
}