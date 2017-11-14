using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.DatabaseFirst.Final.Models
{
    public partial class MasteringEFCoreDbFirstContext : DbContext
    {
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        public MasteringEFCoreDbFirstContext(DbContextOptions<MasteringEFCoreDbFirstContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MasteringEFCoreDbFirst;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.BlogId);

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.BlogId);
            });
        }
    }
}