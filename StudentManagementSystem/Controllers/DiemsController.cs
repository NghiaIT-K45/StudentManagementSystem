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
    public class DiemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diems.Include(d => d.MonHoc).Include(d => d.SinhVien);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Diems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diem = await _context.Diems
                .Include(d => d.MonHoc)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaDiem == id);
            if (diem == null)
            {
                return NotFound();
            }

            return View(diem);
        }

        // GET: Diems/Create
        public IActionResult Create()
        {
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc");
            ViewData["MaSV"] = new SelectList(_context.SinhViens, "MaSV", "TenSV");
            return View();
        }

        // POST: Diems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDiem,MaSV,MaMonHoc,SoDiem,NgayCapNhat")] Diem diem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", diem.MaMonHoc);
            ViewData["MaSV"] = new SelectList(_context.SinhViens, "MaSV", "TenSV", diem.MaSV);
            return View(diem);
        }

        // GET: Diems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diem = await _context.Diems.FindAsync(id);
            if (diem == null)
            {
                return NotFound();
            }
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", diem.MaMonHoc);
            ViewData["MaSV"] = new SelectList(_context.SinhViens, "MaSV", "TenSV", diem.MaSV);
            return View(diem);
        }

        // POST: Diems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDiem,MaSV,MaMonHoc,SoDiem,NgayCapNhat")] Diem diem)
        {
            if (id != diem.MaDiem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiemExists(diem.MaDiem))
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
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", diem.MaMonHoc);
            ViewData["MaSV"] = new SelectList(_context.SinhViens, "MaSV", "TenSV", diem.MaSV);
            return View(diem);
        }

        // GET: Diems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diem = await _context.Diems
                .Include(d => d.MonHoc)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaDiem == id);
            if (diem == null)
            {
                return NotFound();
            }

            return View(diem);
        }

        // POST: Diems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diem = await _context.Diems.FindAsync(id);
            if (diem != null)
            {
                _context.Diems.Remove(diem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiemExists(int id)
        {
            return _context.Diems.Any(e => e.MaDiem == id);
        }
    }
}
