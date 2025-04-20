using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WatchitAPIs.Models;

namespace WatchitAPIs.EFCore
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IConfiguration configuration;
        public AppDbContext(IConfiguration configuration):base()
        {   
            this.configuration = configuration;
        }

        public AppDbContext(DbContextOptions options,IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("cs"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(i => new { i.RoleId, i.UserId });

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(i => new { i.LoginProvider, i.ProviderKey });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(i => new { i.UserId, i.LoginProvider, i.Name });

            modelBuilder.Entity<AppUser>()
                .HasOne(u=>u.Subscription)
                .WithOne(s=>s.User)
                .HasForeignKey<AppUser>(u  => u.SubscriptionId);

            modelBuilder.Entity<FavoriteMovies>()
            .HasKey(um => new { um.UserId, um.MovieId });

            modelBuilder.Entity<FavoriteMovies>()
                .HasOne(um => um.User)
                .WithMany(u => u.UsersFavorite)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<FavoriteMovies>()
                .HasOne(um => um.Movie)
                .WithMany(m => m.FavoriteMovies)
                .HasForeignKey(um => um.MovieId);

            modelBuilder.Entity<MovieCast>()
            .HasKey(um => new { um.CastId, um.MovieId });

            modelBuilder.Entity<MovieCast>()
                .HasOne(um => um.Cast)
                .WithMany(u => u.Movies)
                .HasForeignKey(um => um.CastId);

            modelBuilder.Entity<MovieCast>()
                .HasOne(um => um.Movie)
                .WithMany(m => m.Cast)
                .HasForeignKey(um => um.MovieId);
            
            modelBuilder.Entity<UserWatchRecord>()
                .HasKey(um => new { um.UserId, um.MovieId });
        }
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<FavoriteMovies> FavoriteMovies { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }
        public DbSet<UserWatchRecord> Records { get; set; }
    }
}
