using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Models;

namespace OnTapTuVung.Data
{
    public class Connect : DbContext
    {
        public Connect(DbContextOptions<Connect> options) : base(options)
        {
        }

        public DbSet<HSK> Hsk { get; set; }
        public DbSet<Vocabulary> Vocabulary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình HSK
            modelBuilder.Entity<HSK>(entity =>
            {
                entity.HasKey(e => e.IdHSK);
                entity.Property(e => e.LoaiHSK)
                    .IsRequired()
                    .HasMaxLength(50);

                // Seed dữ liệu HSK
                entity.HasData(
                    new HSK { IdHSK = 1, LoaiHSK = 1 },
                    new HSK { IdHSK = 2, LoaiHSK = 2 },
                    new HSK { IdHSK = 3, LoaiHSK = 3 },
                    new HSK { IdHSK = 4, LoaiHSK = 4 },
                    new HSK { IdHSK = 5, LoaiHSK = 5 },
                    new HSK { IdHSK = 6, LoaiHSK = 6 }
                );
            });

            // Cấu hình Vocabulary
            modelBuilder.Entity<Vocabulary>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TuVung).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Nghia).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Pinyn).HasMaxLength(200);
                entity.Property(e => e.TuLoai).HasMaxLength(50);
                entity.Property(e => e.HanViet).HasMaxLength(200);

                // Quan hệ với HSK
                entity.HasOne(e => e.HSK)
                    .WithMany(e => e.Vocabularies)
                    .HasForeignKey(e => e.IdHSK)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}