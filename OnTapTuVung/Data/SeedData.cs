using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using OnTapTuVung.Models;

namespace OnTapTuVung.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new Connect(
                serviceProvider.GetRequiredService<DbContextOptions<Connect>>());

            // Tạo database nếu chưa có
            context.Database.EnsureCreated();

            // Kiểm tra nếu đã có dữ liệu
            if (context.Hsk.Any())
            {
                return;
            }

            // Thêm 6 cấp độ HSK
            var hskLevels = new[]
            {
                new HSK { LoaiHSK = 1 },
                new HSK { LoaiHSK = 2 },
                new HSK { LoaiHSK = 3 },
                new HSK { LoaiHSK = 4 },
                new HSK { LoaiHSK = 5 },
                new HSK { LoaiHSK = 6 }
            };

            context.Hsk.AddRange(hskLevels);
            context.SaveChanges();

            // Thêm từ vựng mẫu cho HSK1
            var hsk1 = context.Hsk.FirstOrDefault(h => h.LoaiHSK == 1);
            if (hsk1 != null)
            {
                var vocabularies = new[]
                {
                    new Vocabulary
                    {
                        IdHSK = hsk1.IdHSK,
                        TuVung = "你好",
                        Pinyn = "nǐ hǎo",
                        TuLoai = "感叹词",
                        HanViet = "Nhĩ hảo",
                        Nghia = "Xin chào"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk1.IdHSK,
                        TuVung = "谢谢",
                        Pinyn = "xièxiè",
                        TuLoai = "动词",
                        HanViet = "Tạ tạ",
                        Nghia = "Cảm ơn"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk1.IdHSK,
                        TuVung = "再见",
                        Pinyn = "zàijiàn",
                        TuLoai = "动词",
                        HanViet = "Tái kiến",
                        Nghia = "Tạm biệt"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk1.IdHSK,
                        TuVung = "是",
                        Pinyn = "shì",
                        TuLoai = "动词",
                        HanViet = "Thị",
                        Nghia = "Là"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk1.IdHSK,
                        TuVung = "不",
                        Pinyn = "bù",
                        TuLoai = "副词",
                        HanViet = "Bất",
                        Nghia = "Không"
                    }
                };

                context.Vocabulary.AddRange(vocabularies);
                context.SaveChanges();
            }

            // Thêm từ vựng mẫu cho HSK2
            var hsk2 = context.Hsk.FirstOrDefault(h => h.LoaiHSK == 2);
            if (hsk2 != null)
            {
                var vocabularies = new[]
                {
                    new Vocabulary
                    {
                        IdHSK = hsk2.IdHSK,
                        TuVung = "妈妈",
                        Pinyn = "māma",
                        TuLoai = "名词",
                        HanViet = "Ma ma",
                        Nghia = "Mẹ"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk2.IdHSK,
                        TuVung = "爸爸",
                        Pinyn = "bàba",
                        TuLoai = "名词",
                        HanViet = "Ba ba",
                        Nghia = "Bố"
                    },
                    new Vocabulary
                    {
                        IdHSK = hsk2.IdHSK,
                        TuVung = "老师",
                        Pinyn = "lǎoshī",
                        TuLoai = "名词",
                        HanViet = "Lão sư",
                        Nghia = "Giáo viên"
                    }
                };

                context.Vocabulary.AddRange(vocabularies);
                context.SaveChanges();
            }
        }
    }
}