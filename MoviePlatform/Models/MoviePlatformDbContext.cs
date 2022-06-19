using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace MoviePlatform.Models
{
    public class MoviePlatformDbContext : DbContext
    {
        public MoviePlatformDbContext() { }

        public MoviePlatformDbContext(DbContextOptions<MoviePlatformDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<ClosedReport> ClosedReports { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<StarsIn> StarsIns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-H9S9FTJ;Database=MAS;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            mb.Entity<Favorite>(entity =>
            {
                entity.HasKey(f => new { f.IdMovie, f.IdUser });
            });

            mb.Entity<Movie>(entity =>
            {
                entity.Property(m => m.IdMovie).ValueGeneratedOnAdd();

                entity.HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.Roles)
                .WithOne(r => r.Movie)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .Property(m => m.Genres)
                .HasConversion(
                 v => string.Join(',', v),
                 v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            });

            mb.Entity<Person>(entity =>
            {
                entity.Property(p => p.IdPerson).ValueGeneratedOnAdd();

                entity.HasMany(p => p.MoviesDirected)
                .WithOne(m => m.Director)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.MoviesWritten)
                .WithOne(m => m.Writer)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Roles)
                .WithOne(r => r.Actor)
                .OnDelete(DeleteBehavior.Restrict);

                entity
               .Property(p => p.Types)
               .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            });

            mb.Entity<Report>(entity =>
            {
                entity.Property(r => r.IdReport).ValueGeneratedOnAdd();
            });

            mb.Entity<Review>(entity =>
            {
                entity.Property(p => p.IdReview).ValueGeneratedOnAdd();
            });

            mb.Entity<StarsIn>(entity =>
            {
                entity.HasKey(si => new { si.IdMovie, si.IdPerson });
            });

            mb.Entity<User>(entity =>
            {
                entity.Property(u => u.IdUser).ValueGeneratedOnAdd();

                entity.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Favorites)
                .WithOne(f => f.User)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.ReportsSent)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.ReportsClosed)
                .WithOne(r => r.Moderator)
                .OnDelete(DeleteBehavior.NoAction);          
            });

            mb.Seed();
        }
    }
}
