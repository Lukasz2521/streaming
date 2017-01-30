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

            string userId = User.Identity.GetUserId().ToString();
            await userSongs.saveSongAsync(userId, songUpload.Title, songUploadDate.ToString());
            songUpload.Avatar.SaveAs(Helpers.Helpers.getAvatarPath(userId, userSongs.getSongTableCount()));
            this.SaveFile(22, 22, fileType.song, songUpload.Song);
            this.SaveFile(22, 22, fileType.avatar, songUpload.Avatar);

            return RedirectToAction("Index", "Home");
        }

        public void PlaySong()
        {
            IWavePlayer waveOutDevice;
            AudioFileReader audioFileReader;

            waveOutDevice = new WaveOut();
           // var songPath = Helpers.Helpers.GetHomePath() + "/Music/buka.mp3";
            audioFileReader = new AudioFileReader("buka.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        #region Helpers

        private void SaveFile(int userId, int songId, fileType typeOfFile, HttpPostedFileBase file)
        {
            switch (typeOfFile)
            {
                case fileType.avatar:
                    {
                        file.SaveAs(Helpers.Helpers.getAvatarPath(userId.ToString(), songId));
                    }
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