using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streaming_inż.Models;

namespace streaming_inż.Common
{
    public interface ISongRepository
    {
        IEnumerable<UserSong> getAllUserSongs(string userId);

        Task saveSongAsync(string userId, string title, string publicDate);

        IEnumerable<UserSong> GetFavoriteSongs(string userId);

        Task AddFavoriteSongAsync(LikedSong likedSong);

        int getSongTableCount();

        List<string> findByKeyword(string keyWord, string userId);

        IEnumerable<UserSong> getByKeyword(string keyWord, string userId);

        void removeSong(int songId);

        void ExtractSampleFromSong(ExtractFile file);

        IEnumerable<UserSong> GetWaitingSongs();
    }
}
