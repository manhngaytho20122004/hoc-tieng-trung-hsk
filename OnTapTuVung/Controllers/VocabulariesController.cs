using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using OnTapTuVung.Data;
using OnTapTuVung.Models;

namespace OnTapTuVung.Controllers
{
    public class VocabularyController : Controller
    {
        private readonly Connect _context;

        public VocabularyController(Connect context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int hskId, int level)
        {
            ViewBag.HSKId = hskId;
            ViewBag.HSKLevel = level;

            var vocabularies = await _context.Vocabulary
                .Where(v => v.IdHSK == hskId)
                .OrderBy(v => v.Id)
                .ToListAsync();

            return View(vocabularies);
        }

        // Thêm từ vựng mới
        [HttpPost]
        public async Task<IActionResult> Create(int hskId, string tuVung, string pinyn, string tuLoai, string hanViet, string nghia)
        {
            try
            {
                var vocabulary = new Vocabulary
                {
                    IdHSK = hskId,
                    TuVung = tuVung,
                    Pinyn = pinyn,
                    TuLoai = tuLoai ?? "",
                    HanViet = hanViet ?? "",
                    Nghia = nghia
                };

                _context.Vocabulary.Add(vocabulary);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Thêm từ vựng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // Sửa từ vựng
        [HttpPost]
        public async Task<IActionResult> Update(int id, string tuVung, string pinyn, string tuLoai, string hanViet, string nghia)
        {
            try
            {
                var vocabulary = await _context.Vocabulary.FindAsync(id);
                if (vocabulary == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy từ vựng!" });
                }

                vocabulary.TuVung = tuVung;
                vocabulary.Pinyn = pinyn;
                vocabulary.TuLoai = tuLoai ?? "";
                vocabulary.HanViet = hanViet ?? "";
                vocabulary.Nghia = nghia;

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật từ vựng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // Xóa từ vựng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var vocabulary = await _context.Vocabulary.FindAsync(id);
                if (vocabulary == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy từ vựng!" });
                }

                _context.Vocabulary.Remove(vocabulary);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Xóa từ vựng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // Lấy thông tin chi tiết 1 từ
        [HttpGet]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var vocabulary = await _context.Vocabulary.FindAsync(id);
                if (vocabulary == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy từ vựng!" });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        vocabulary.Id,
                        vocabulary.TuVung,
                        vocabulary.Pinyn,
                        vocabulary.TuLoai,
                        vocabulary.HanViet,
                        vocabulary.Nghia
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Action cho trang Quiz nhận tham số type
        public IActionResult Quiz(int hskId, int level, int type = 2)
        {
            ViewBag.HSKId = hskId;
            ViewBag.HSKLevel = level;
            ViewBag.QuizType = type; // 0: Trung→Việt, 1: Việt→Trung, 2: Trộn lẫn
            return View();
        }

        // Sửa lại GetQuizQuestions để nhận tham số type
        [HttpGet]
        public async Task<IActionResult> GetQuizQuestions(int hskId, int questionCount = 10, int type = 2)
        {
            try
            {
                var vocabularies = await _context.Vocabulary
                    .Where(v => v.IdHSK == hskId)
                    .ToListAsync();

                if (vocabularies == null || !vocabularies.Any())
                {
                    return Json(new { success = false, message = "Chưa có từ vựng để kiểm tra!" });
                }

                var random = new Random();
                var selectedVocabs = vocabularies
                    .OrderBy(x => random.Next())
                    .Take(Math.Min(questionCount, vocabularies.Count))
                    .ToList();

                var questions = new List<object>();

                foreach (var v in selectedVocabs)
                {
                    int questionType;

                    if (type == 0) // Chỉ Trung → Việt
                    {
                        questionType = 0;
                    }
                    else if (type == 1) // Chỉ Việt → Trung
                    {
                        questionType = 1;
                    }
                    else // Trộn lẫn (type == 2)
                    {
                        questionType = random.Next(0, 2);
                    }

                    string questionText = questionType == 0 ? v.TuVung : v.Nghia;
                    string correctAnswer = questionType == 0 ? v.Nghia : v.TuVung;

                    questions.Add(new
                    {
                        id = v.Id,
                        type = questionType,
                        questionText = questionText,
                        correctAnswer = correctAnswer,
                        tuVung = v.TuVung,
                        nghia = v.Nghia,
                        pinyn = v.Pinyn
                    });
                }

                return Json(new { success = true, questions });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}