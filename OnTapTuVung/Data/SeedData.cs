using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Models;

namespace OnTapTuVung.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = new Connect(
                serviceProvider.GetRequiredService<DbContextOptions<Connect>>());

            // Kiểm tra nếu đã có dữ liệu
            if (await context.Vocabulary.AnyAsync())
            {
                Console.WriteLine("Database already has data, skipping seed...");
                return;
            }

            Console.WriteLine("Seeding database...");

            // Seed dữ liệu mẫu cho HSK 1
            var vocabularies = new List<Vocabulary>
            {
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "你好",
                    Pinyn = "nǐ hǎo",
                    TuLoai = "thán từ",
                    HanViet = "nhĩ hảo",
                    Nghia = "Xin chào"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "谢谢",
                    Pinyn = "xiè xie",
                    TuLoai = "động từ",
                    HanViet = "tạ tạ",
                    Nghia = "Cảm ơn"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "对不起",
                    Pinyn = "duì bù qǐ",
                    TuLoai = "cụm từ",
                    HanViet = "đối bất khởi",
                    Nghia = "Xin lỗi"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "是",
                    Pinyn = "shì",
                    TuLoai = "động từ",
                    HanViet = "thị",
                    Nghia = "Là"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "不",
                    Pinyn = "bù",
                    TuLoai = "phó từ",
                    HanViet = "bất",
                    Nghia = "Không"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "我",
                    Pinyn = "wǒ",
                    TuLoai = "đại từ",
                    HanViet = "ngã",
                    Nghia = "Tôi"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "你",
                    Pinyn = "nǐ",
                    TuLoai = "đại từ",
                    HanViet = "nhĩ",
                    Nghia = "Bạn"
                },
                new Vocabulary
                {
                    IdHSK = 1,
                    TuVung = "好",
                    Pinyn = "hǎo",
                    TuLoai = "tính từ",
                    HanViet = "hảo",
                    Nghia = "Tốt"
                }
            };

            await context.Vocabulary.AddRangeAsync(vocabularies);
            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded {vocabularies.Count} vocabularies for HSK 1");
        }

        // Giữ lại phương thức cũ cho tương thích
        public static void Initialize(IServiceProvider serviceProvider)
        {
            InitializeAsync(serviceProvider).GetAwaiter().GetResult();
        }
    }
}