using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace streaming_inż.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        public int YearOfBirth { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }


    public class Song
    {
        public int SongID { get; set; }

        public string Title { get; set; }
        
        public int Avatar { get; set; }

        public DataType PublicDate { get; set; }

        public virtual ApplicationUser Users { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }

    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual Song Songs { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}