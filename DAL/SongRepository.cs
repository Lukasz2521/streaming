using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using streaming_inż.Models;
using Microsoft.AspNet.Identity;
using NAudio.Wave;
using System.Web.Mvc;

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

        public List<string> findByKeyword(string keyWord, string userId)
        {
            var matchedWords = context.Song
                .Where(s => s.Title.Contains(keyWord) && s.userId != userId)
                .Select(s => s.Title).ToList();

            return matchedWords;
        }

        public IEnumerable<UserSongs> getByKeyword(string keyWord, string userId)
        {
            IEnumerable<UserSongs> matchedSongs = context.Song
                .Where(s => s.Title.Contains(keyWord) && s.userId != userId)
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

        public void removeSong(int songId)
        {
            Song song = context.Song.First(s => s.SongID == songId);

            context.Song.Remove(song);
            context.SaveChanges();
        }

        public void ExtractSampleFromSong(ExtractFile file)
        {
            using (var mp3FileReader = new Mp3FileReader(String.Concat(@"D:\Streaming_Data\Songs\", file.songId, ".mp3")))
            using (var writer = File.Create(String.Concat(@"D:\Streaming_Data\Extract\", file.songId, ".mp3")))
            {
                var startPosition = TimeSpan.FromSeconds(file.cutFrom);
                var endPosition = TimeSpan.FromSeconds(file.cutTo);

                mp3FileReader.CurrentTime = startPosition;
                while (mp3FileReader.CurrentTime < endPosition)
                {
                    var frame = mp3FileReader.ReadNextFrame();
                    if (frame == null) break;
                    writer.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }

        public IEnumerable<UserSongs> GetFavoriteSongs(string userId)
        {
            var favoriteSongs = (from song in context.Song
                                 join likedSong in context.LikedSongs on song.SongID equals likedSong.SongId
                                 where likedSong.UserId == userId
                                 select new UserSongs
                                 {
                                     SongId = String.Concat(song.userId, "_", song.SongID),
                                     Title = song.Title,
                                     UserName = song.User.UserName,
                                     avatarPath = String.Concat("/avatars/", song.userId, "_", song.SongID, ".jpg"),
                                     UploadTime = song.PublicDate
                                 }).ToList();

            return favoriteSongs;
        }

        public async Task AddFavoriteSongAsync(LikedSong likedSong)
        {
            int likedSongCount = context.LikedSongs.Where(s => s.SongId == likedSong.SongId && s.UserId == likedSong.UserId).Count();

            if(likedSongCount == 0)
            {
                var _likedSong = new LikedSongs()
                {
                    SongId = likedSong.SongId,
                    UserId = likedSong.UserId
                };

                context.LikedSongs.Add(_likedSong);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<UserSongs> GetWaitingSongs()
        {
            var songsToAccept = context.Song.Where(s => s.isAccepted != true)
                                       .Select(s => new UserSongs()
                                       {
                                           SongId = String.Concat(s.userId, "_", s.SongID),
                                           Title = s.Title,
                                           UserName = s.User.UserName,
                                           avatarPath = String.Concat("/avatars/", s.userId, "_", s.SongID, ".jpg"),
                                           UploadTime = s.PublicDate
                                       });

            return songsToAccept;
        }
    }
}