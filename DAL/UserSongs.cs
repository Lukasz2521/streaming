using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using streaming_inż.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace streaming_inż.DAL
{
    public class UserSongs_DAL
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<Models.UserSongs> getAllSongs(string userId)
        {
            List<Models.UserSongs> userSongsModel = new List<Models.UserSongs>();
            Models.UserSongs _song;
            var userSongs = context.Song.Where(x => x.userId == userId).ToList();                                      
                                                                 
            if(userSongs.Count > 0)
            {
                foreach(var song in userSongs)
                {
                    _song = new Models.UserSongs()
                    {
                        Title = song.Title,
                        avatarPath = Helpers.Helpers.getAvatarPath(song.userId, song.SongID),
                        UploadTime = Helpers.Helpers.getTimeStamp(DateTime.Parse(song.PublicDate))
                    };
                    userSongsModel.Add(_song);
                }
            }

            return userSongsModel;
        }

        public async Task saveSongAsync(string userId , string title, string publicDate)
        {
            var song = new Song()
            {
                userId = userId,
                Title = title,
                PublicDate = publicDate
            };
                                            
            context.Song.Add(song);
            await context.SaveChangesAsync();
        }     
        
        public int getSongTableCount()
        {
            return context.Song.Count();
        }                   
    }
}