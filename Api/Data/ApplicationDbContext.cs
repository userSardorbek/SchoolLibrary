using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
        public DbSet<User> User { get; set; }
}