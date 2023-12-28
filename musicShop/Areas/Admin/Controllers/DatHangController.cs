using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicShop.Models;

namespace musicShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DatHangController : Controller
    {
        private readonly musicShopDbContext _context;

        public DatHangController (musicShopDbContext context)
        {
            _context = context;
        }

        // GET: DatHang
        public async Task<IActionResult> Index ()
        {
            var musicShopDbContext = _context.DatHang.Include(d => d.NguoiDung).Include(d => d.TinhTrang);
            return View(await musicShopDbContext.ToListAsync());
        }

        // GET: DatHang/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null || _context.DatHang == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHang
                .Include(d => d.NguoiDung)
                .Include(d => d.TinhTrang)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // GET: DatHang/Create
        public IActionResult Create ()
        {
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "ID");
            ViewData["TinhTrangID"] = new SelectList(_context.KhachHang, "ID", "ID");
            return View();
        }

        // POST: DatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("ID,NguoiDungID,TinhTrangID,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "ID", datHang.NguoiDungID);
            ViewData["TinhTrangID"] = new SelectList(_context.KhachHang, "ID", "ID", datHang.TinhTrangID);
            return View(datHang);
        }

        // GET: DatHang/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null || _context.DatHang == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHang.FindAsync(id);
            if (datHang == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "ID", datHang.NguoiDungID);
            ViewData["TinhTrangID"] = new SelectList(_context.KhachHang, "ID", "ID", datHang.TinhTrangID);
            return View(datHang);
        }

        // POST: DatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind("ID,NguoiDungID,TinhTrangID,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang")] DatHang datHang)
        {
            if (id != datHang.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHangExists(datHang.ID))
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
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "ID", datHang.NguoiDungID);
            ViewData["TinhTrangID"] = new SelectList(_context.KhachHang, "ID", "ID", datHang.TinhTrangID);
            return View(datHang);
        }

        // GET: DatHang/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null || _context.DatHang == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHang
                .Include(d => d.NguoiDung)
                .Include(d => d.TinhTrang)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // POST: DatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            if (_context.DatHang == null)
            {
                return Problem("Entity set 'musicShopDbContext.DatHang'  is null.");
            }
            var datHang = await _context.DatHang.FindAsync(id);
            if (datHang != null)
            {
                _context.DatHang.Remove(datHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatHangExists (int id)
        {
            return (_context.DatHang?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
