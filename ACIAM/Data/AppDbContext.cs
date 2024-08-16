using ACIAM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACIAM.Data
{
	public class AppDbContext : IdentityDbContext
	{
        public AppDbContext(DbContextOptions options) : base(options)
        {   
            
        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Speaker> Speakers { get; set; }    
    }
}
