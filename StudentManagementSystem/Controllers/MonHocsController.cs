using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class MonHocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonHocsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MonHocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MonHocs.Include(m => m.KhoaHoc);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MonHocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .Include(m => m.KhoaHoc)
                .FirstOrDefaultAsync(m => m.MaMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // GET: MonHocs/Create
        public IActionResult Create()
        {
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc");
            return View();
        }

        // POST: MonHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMonHoc,TenMonHoc,MoTa,MaKhoaHoc")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", monHoc.MaKhoaHoc);
            return View(monHoc);
        }

        // GET: MonHocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc == null)
            {
                return NotFound();
            }
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", monHoc.MaKhoaHoc);
            return View(monHoc);
        }

        // POST: MonHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMonHoc,TenMonHoc,MoTa,MaKhoaHoc")] MonHoc monHoc)
        {
            if (id != monHoc.MaMonHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonHocExists(monHoc.MaMonHoc))
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
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", monHoc.MaKhoaHoc);
            return View(monHoc);
        }

        // GET: MonHocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .Include(m => m.KhoaHoc)
                .FirstOrDefaultAsync(m => m.MaMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // POST: MonHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc != null)
            {
                _context.MonHocs.Remove(monHoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonHocExists(int id)
        {
            return _context.MonHocs.Any(e => e.MaMonHoc == id);
        }
    }
}
