using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Artist;
using musicApi2.Models.Release;
using musicApi2.Models.ArtistsReleases;
using musicApi2.Models.Genre;
using musicApi2.Models.Rating;
using musicApi2.Models.Review;
using musicApi2.Models.User;

namespace musicApi2.Services
{
    public class musicApiContext : DbContext
    {
        public musicApiContext(DbContextOptions<musicApiContext> options) : base(options)
        {
        }

        public DbSet<Release> Releases { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ArtistsReleases>().Ignore(b => b.Artists);
            //    //modelBuilder.Entity<Artist>()
            //    //    .HasMany(a => a.ArtistsReleases)
            //    //    .
            //    //modelBuilder.Entity<Artist>()
            //    //    .HasMany(a => a.ArtistsReleases)
            //    //    .WithOne(ar => ar.Artist)
            //    //    .HasForeignKey(ar => ar.ArtistId);

            //    //modelBuilder.Entity<Release>()
            //    //    .HasMany(r => r.ArtistsReleases)
            //    //    .WithOne(ar => ar.Release)
            //    //    .HasForeignKey(ar => ar.ReleaseId);

            //    //modelBuilder.Entity<ArtistsReleases>()

        }
    }
}
