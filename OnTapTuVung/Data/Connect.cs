using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Models;

namespace OnTapTuVung.Data
{
    public class Connect : DbContext
    {
        public Connect(DbContextOptions<Connect> options)
            : base(options)
        {
        }

        public DbSet<HSK> Hsk { get; set; }
        public DbSet<Vocabulary> Vocabulary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ
            modelBuilder.Entity<Vocabulary>()
                .HasOne(v => v.HSK)
                .WithMany(h => h.Vocabularies)
                .HasForeignKey(v => v.IdHSK);
        }
    }
}