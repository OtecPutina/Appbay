using Appbay.Models;
using Microsoft.EntityFrameworkCore;

namespace Appbay.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Model> Models { get; set; }
    }
}
