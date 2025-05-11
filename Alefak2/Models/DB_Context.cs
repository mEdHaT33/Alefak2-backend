using Alefak2.Models;
using Microsoft.EntityFrameworkCore;

namespace Alefak2.Models
{
    public class ApiContext : DbContext
    {

      public ApiContext(DbContextOptions options) : base(options)
      {
        
      }

        public DbSet<Posts> posts {  get; set; }
        public DbSet<User> users { get; set; }

    }
}
