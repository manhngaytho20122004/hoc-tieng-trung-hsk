using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Data;
using OnTapTuVung.Models;

namespace OnTapTuVung.Controllers
{
    public class HSKsController : Controller
    {
        private readonly Connect _context;

        public HSKsController(Connect context)
        {
            _context = context;
        }

        // GET: HSKs
        public async Task<IActionResult> Index()
        {
              return _context.Hsk != null ? 
                          View(await _context.Hsk.ToListAsync()) :
                          Problem("Entity set 'Connect.Hsk'  is null.");
        }

        // GET: HSKs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hsk == null)
            {
                return NotFound();
            }

            var hSK = await _context.Hsk
                .FirstOrDefaultAsync(m => m.IdHSK == id);
            if (hSK == null)
            {
                return NotFound();
            }

            return View(hSK);
        }

        // GET: HSKs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HSKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHSK,LoaiHSK")] HSK hSK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hSK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hSK);
        }

        // GET: HSKs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hsk == null)
            {
                return NotFound();
            }

            var hSK = await _context.Hsk.FindAsync(id);
            if (hSK == null)
            {
                return NotFound();
            }
            return View(hSK);
        }

        // POST: HSKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHSK,LoaiHSK")] HSK hSK)
        {
            if (id != hSK.IdHSK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hSK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HSKExists(hSK.IdHSK))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hSK);
        }

        // GET: HSKs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hsk == null)
            {
                return NotFound();
            }

            var hSK = await _context.Hsk
                .FirstOrDefaultAsync(m => m.IdHSK == id);
            if (hSK == null)
            {
                return NotFound();
            }

            return View(hSK);
        }

        // POST: HSKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hsk == null)
            {
                return Problem("Entity set 'Connect.Hsk'  is null.");
            }
            var hSK = await _context.Hsk.FindAsync(id);
            if (hSK != null)
            {
                _context.Hsk.Remove(hSK);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HSKExists(int id)
        {
          return (_context.Hsk?.Any(e => e.IdHSK == id)).GetValueOrDefault();
        }
    }
}
