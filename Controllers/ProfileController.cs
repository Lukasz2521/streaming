using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NAudio;
using NAudio.Wave;
using streaming_inż.Helpers;
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
        private UserSongs_DAL userSongs = new UserSongs_DAL();


      // GET: Profile
        [OutputCache(Duration = 60)]
        public ActionResult Index(string userName)
        {
            var allUserSongsModel = userSongs.getAllSongs(User.Identity.GetUserId());
                                                     
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
            DateTime songUploadDate = DateTime.Today;
            
            if(ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId().ToString();
                await userSongs.saveSongAsync(userId, songUpload.Title, songUploadDate.ToString());
                int lastAddedSongId = userSongs.getSongTableCount();
                songUpload.Avatar.SaveAs(Helpers.Helpers.getAvatarPath(userId, lastAddedSongId));
                songUpload.Song.SaveAs(Helpers.Helpers.getSongPath(userId, lastAddedSongId));

                return RedirectToAction("Index", "Home");
            }
            else
            { 
                return View(songUpload);
            }
    
        }

        public void PlaySongAsync()
        {
            Thread song = new Thread(this.PlaySong);
        }

        public void PlaySong()
        {
            IWavePlayer waveOutDevice;
            waveOutDevice = new WaveOutEvent();
            var audioFileReader = new Mp3FileReader(@"\songs\30852a05-bb23-4936-92b9-751667b73986_31.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        #region Helpers

        private void SaveFile(int userId, int songId, fileType typeOfFile, HttpPostedFileBase file)
        {
            switch (typeOfFile)
            {
                case fileType.avatar:
                    file.SaveAs(Helpers.Helpers.getAvatarPath(userId.ToString(), songId));    
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