using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using streaming_inż.Models;
using Microsoft.AspNet.Identity;
using NAudio.Wave;

namespace streaming_inż.DAL
{
    public class SongRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<UserSongs> getAllUserSongs(string userId)
        {
            IEnumerable<UserSongs> userSongs = context.Song
                .Where(s => s.userId == userId)
                .Select(s => new UserSongs
                {
                    SongId = String.Concat(s.userId, "_", s.SongID),
                    UserName = s.User.UserName,
                    Title = s.Title,
                    avatarPath = String.Concat("/avatars/", s.userId, "_", s.SongID, ".jpg"),
                    UploadTime = s.PublicDate
                })
                .ToList();

            return userSongs;
        }

        public async Task saveSongAsync(string userId, string title, string publicDate)
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
            int songId = context.Song.OrderByDescending(x => x.SongID).FirstOrDefault().SongID;

            return songId;
        }

        public List<string> findByKeyword(string keyWord)
        {
            var matchedWords = context.Song.Where(s => s.Title.Contains(keyWord)).Select(s => s.Title).ToList();

            return matchedWords;
        }

        public IEnumerable<UserSongs> getByKeyword(string keyWord)
        {
            IEnumerable<UserSongs> matchedSongs = context.Song
                .Where(s => s.Title.Contains(keyWord))
                .Select(s => new UserSongs
                {
                    SongId = String.Concat(s.userId, "_", s.SongID),
                    Title = s.Title,
                    UserName = s.User.UserName,
                    avatarPath = String.Concat("/avatars/", s.userId, "_", s.SongID, ".jpg"),
                    UploadTime = s.PublicDate
                })
                .ToList();

            return matchedSongs;
        }

        public void ExtractSampleFromSong(int from, int to, string songID)
        {
            using (var mp3FileReader = new Mp3FileReader(@"\songs\30852a05-bb23-4936-92b9-751667b73986_31.mp3"))
            using (var writer = File.Create(@"\songs\extract.mp3"))
            {
                var startPosition = TimeSpan.FromSeconds(30);
                var endPosition = TimeSpan.FromSeconds(60);

                mp3FileReader.CurrentTime = startPosition;
                while (mp3FileReader.CurrentTime < endPosition)
                {
                    var frame = mp3FileReader.ReadNextFrame();
                    if (frame == null) break;
                    writer.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }
    }
}