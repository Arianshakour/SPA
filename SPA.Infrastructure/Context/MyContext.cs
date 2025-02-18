using Microsoft.EntityFrameworkCore;
using SPA.Domain.Entities;

namespace SPA.Infrastructure.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
