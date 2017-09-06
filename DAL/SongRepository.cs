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
using streaming_inż.Common;

namespace streaming_inż.DAL
{
    public class SongRepository : ISongRepository
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<UserSong> getAllUserSongs(string userId)
        {
            IEnumerable<UserSong> userSongs = _context.Song
                .Where(s => s.UserId == userId && s.isAccepted)
                .Select(s => new UserSong
                {
                    SongId = s.SongId,
                    UserName = s.User.UserName,
                    Title = s.Title,
                    avatarPath = String.Concat("/avatars/", s.UserId, "_", s.SongId, ".jpg"),
                    UploadTime = s.PublicDate
                })
                .ToList();

            return userSongs;
        }

        public async Task saveSongAsync(string userId, string title, string publicDate)
        {
            var song = new Song()
            {
                UserId = userId,
                Title = title,
                PublicDate = publicDate,
                isAccepted = false
            };

            _context.Song.Add(song);
            await _context.SaveChangesAsync();
        }

        public int getSongTableCount()
        {
            int songId = _context.Song.OrderByDescending(x => x.SongId).FirstOrDefault().SongId;

            return songId;
        }

        public List<string> findByKeyword(string keyWord, string userId)
        {
            var matchedWords = _context.Song
                                .Where(s => s.Title.Contains(keyWord) && s.UserId != userId)
                                .Select(s => s.Title).ToList();

            return matchedWords;
        }

        public IEnumerable<UserSong> getByKeyword(string keyWord, string userId)
        {
            IEnumerable<UserSong> matchedSongs = _context.Song
                .Where(s => s.Title.Contains(keyWord) && s.UserId != userId)
                .Select(s => new UserSong
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    UserName = s.User.UserName,
                    avatarPath = String.Concat("/avatars/", s.UserId, "_", s.SongId, ".jpg"),
                    UploadTime = s.PublicDate
                })
                .ToList();

            return matchedSongs;
        }     

        public void removeSong(int songId)
        {
            Song song = _context.Song.First(s => s.SongId == songId);

            _context.Song.Remove(song);
            _context.SaveChanges();
        }

        public void ExtractSampleFromSong(ExtractFile file)
        {
            using (var mp3FileReader = new Mp3FileReader(String.Concat(@"D:\Streaming_Data\Songs\", file.SongId, ".mp3")))
            using (var writer = File.Create(String.Concat(@"D:\Streaming_Data\Extract\", file.SongId, ".mp3")))
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

        public IEnumerable<UserSong> GetFavoriteSongs(string userId)
        {
            var favoriteSongs = (from song in _context.Song
                                 join likedSong in _context.LikedSong on song.SongId equals likedSong.SongId
                                 where likedSong.UserId == userId
                                 select new UserSong
                                 {
                                     SongId = song.SongId,
                                     Title = song.Title,
                                     UserName = song.User.UserName,
                                     avatarPath = String.Concat("/avatars/", song.UserId, "_", song.SongId, ".jpg"),
                                     UploadTime = song.PublicDate
                                 }).ToList();

            return favoriteSongs;
        }

        public async Task AddFavoriteSongAsync(LikedSong likedSong)
        {
            int likedSongCount = _context.LikedSong.Where(s => s.SongId == likedSong.SongId && s.UserId == likedSong.UserId).Count();

            if(likedSongCount == 0)
            {
                var _likedSong = new LikedSong()
                {
                    SongId = likedSong.SongId,
                    UserId = likedSong.UserId
                };

                _context.LikedSong.Add(_likedSong);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<UserSong> GetWaitingSongs()
        {
            var songsToAccept = _context.Song.Where(s => s.isAccepted != true)
                                       .Select(s => new UserSong()
                                       {
                                           SongId = s.SongId,
                                           Title = s.Title,
                                           UserName = s.User.UserName,
                                           avatarPath = String.Concat("/avatars/", s.UserId, "_", s.SongId, ".jpg"),
                                           UploadTime = s.PublicDate
                                       }).ToList();

            return songsToAccept;
        }
    }
}