using DunkSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DunkSearch.Domain
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caption>()
                .HasGeneratedTsVectorColumn(
                    p => p.CaptionTextVector,
                    "english",  // Text search config
                    p => new { p.CaptionText })  // Included properties
                .HasIndex(p => p.CaptionTextVector)
                .HasMethod("GIST"); // Index method on the search vector (GIN or GIST)

            modelBuilder.Entity<Caption>()
                .HasGeneratedTsVectorColumn(
                    p => p.CaptionTextSimpleVector,
                    "simple",  // Text search config
                    p => new { p.CaptionText })  // Included properties
                .HasIndex(p => p.CaptionTextSimpleVector)
                .HasMethod("GIST"); // Index method on the search vector (GIN or GIST)
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<CaptionType> CaptionTypes { get; set; }
        public DbSet<Caption> Captions { get; set; }
        public DbSet<AppEventLog> AppEventLogs { get; set; }
        public DbSet<UnsupportedVideo> UnsupportedVideos { get; set; }
    }
}
