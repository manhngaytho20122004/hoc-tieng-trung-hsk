using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using OnTapTuVung.Data;
using OnTapTuVung.Models;

namespace OnTapTuVung.Controllers
{
    public class HomeController : Controller
    {
        private readonly Connect _context;

        public HomeController(Connect context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Action để test database
        public async Task<IActionResult> Test()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var message = $"Kết nối database: {(canConnect ? "OK" : "FAILED")}\n";

                if (canConnect)
                {
                    var hskCount = await _context.Hsk.CountAsync();
                    var vocabCount = await _context.Vocabulary.CountAsync();
                    message += $"Số lượng HSK: {hskCount}\n";
                    message += $"Số lượng Vocabulary: {vocabCount}\n";

                    // Lấy danh sách HSK
                    var hsks = await _context.Hsk.ToListAsync();
                    foreach (var hsk in hsks)
                    {
                        message += $"HSK {hsk.LoaiHSK}: ID={hsk.IdHSK}\n";
                    }
                }

                return Content(message, "text/plain");
            }
            catch (System.Exception ex)
            {
                return Content($"Error: {ex.Message}\n{ex.StackTrace}", "text/plain");
            }
        }

        // Action để lấy danh sách HSK
        [HttpGet]
        public async Task<IActionResult> GetHSKLevels()
        {
            try
            {
                System.Console.WriteLine("GetHSKLevels called");

                // Kiểm tra kết nối
                var canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    return Json(new { error = "Không thể kết nối đến database" });
                }

                // Lấy danh sách HSK
                var hskLevels = await _context.Hsk
                    .Select(h => new
                    {
                        h.IdHSK,
                        h.LoaiHSK,
                        VocabularyCount = _context.Vocabulary.Count(v => v.IdHSK == h.IdHSK)
                    })
                    .OrderBy(h => h.LoaiHSK)
                    .ToListAsync();

                System.Console.WriteLine($"Found {hskLevels.Count} HSK levels");

                if (hskLevels == null || !hskLevels.Any())
                {
                    return Json(new { error = "Chưa có dữ liệu HSK. Vui lòng thêm dữ liệu!" });
                }

                return Json(hskLevels);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error in GetHSKLevels: {ex.Message}");
                return Json(new { error = $"Lỗi server: {ex.Message}" });
            }
        }
    }
}