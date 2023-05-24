using FastMail.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace FastMail.Web
{
    public class FastMailContext : DbContext
    {
        public FastMailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Sender> Senders { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<SpecialPackage> SpecialPackages { get; set; }
        public DbSet<User> Users { get; set; }

        }
    }