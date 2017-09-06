using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using streaming_inż.Common;
using streaming_inż.Models;

namespace streaming_inż.DAL
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public void UpdateSettings(ApplicationUser user)
        {
            var userData = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            userData = user;
            _context.SaveChanges();
        }
    }
}