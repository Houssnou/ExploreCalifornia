using ExploreCalifornia.DataAccess.Models;

namespace ExploreCalifornia.DataAccess
{
    using System.Data.Entity;

    public partial class AppDataContext : DbContext
    {
        public AppDataContext() : base("name=AppDataContext")
        {
            this.Configuration.LazyLoadingEnabled = false;  
        }

        public virtual DbSet<AuthorizedApp> AuthorizedApps { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // AuthorizedApp
            modelBuilder.Entity<AuthorizedApp>()
                .Property(e => e.AppToken)
                .IsUnicode(false);
            modelBuilder.Entity<AuthorizedApp>()
                .Property(e => e.AppSecret)
                .IsUnicode(false);

            // Tour
            modelBuilder.Entity<Tour>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
