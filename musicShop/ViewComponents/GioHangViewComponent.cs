using musicShop.Logic;
using musicShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoShop.ViewComponents
{
    public class GioHangViewComponent : ViewComponent
    {
        private readonly musicShopDbContext _context;
        public GioHangViewComponent (musicShopDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke ()
        {
            GioHangLogic gioHangLogic = new GioHangLogic(_context);
            decimal tongTien = gioHangLogic.LayTongTienSanPham();
            decimal tongSoLuong = gioHangLogic.LayTongSoLuong();
            TempData["TopMenu_TongTien"] = tongTien;
            TempData["TopMenu_TongSoLuong"] = tongSoLuong;
            return View("Default");
        }
    }
}