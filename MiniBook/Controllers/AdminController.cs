using MiniBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MiniBook.Controllers
{
    public class AdminController : Controller
    {
        BOOKSTOREEntities db=new BOOKSTOREEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login","Admin");
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Login(QUANLI admin)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra có admin này hay chưa
                var adminDB = db.QUANLIs.FirstOrDefault(ad => ad.TenTaiKhoan ==admin.TenTaiKhoan && ad.MatKhau == admin.MatKhau);
                if (adminDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Admin"] = adminDB;
                    Session["AdminName"] = adminDB.TenTaiKhoan;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.ThongBao = "Sai tên đăng nhập hoặc mật khẩu";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult ThongKe()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            int thang = DateTime.Now.Month;
            //Số đơn hàng thành công trong tháng
            int slDonHangThanhCongTrongThang=db.DONHANGs.Where(d => d.TrangThai == "Đã hoàn thành" && d.NgayDat.Value.Month==thang).Count();
            ViewBag.slDonHangThanhCongTrongThang = slDonHangThanhCongTrongThang;
            //Số đơn hàng trong tháng 
            int slDonHangTrongThang = db.DONHANGs.Where(d => d.NgayDat.Value.Month == thang).Count();
            ViewBag.slDonHangTrongThang=slDonHangTrongThang;
            //Số đơn hàng đã hủy trong tháng
            int slDonHangHuyTrongThang = db.DONHANGs.Where(d => d.TrangThai == "Đã hủy" && d.NgayDat.Value.Month == thang).Count();
            ViewBag.slDonHangHuyTrongThang = slDonHangHuyTrongThang;
            // số đơn trạng thái chờ
            int slDonCho = slDonHangTrongThang - slDonHangHuyTrongThang - slDonHangThanhCongTrongThang;
            ViewBag.slDonCho = slDonCho;
            //Doanh thu trong tháng
            double doanhThuThang = db.DONHANGs.Where(d => d.TrangThai == "Đã hoàn thành" && d.NgayDat.Value.Month == thang).Sum(d => d.TongTien.Value);
            ViewBag.doanhThuThang = doanhThuThang;
            //Doanh thu dự tính cho đến hiện tại trong tháng
            double doanhThuThangDuTinh = db.DONHANGs.Where(d => d.TrangThai != "Đã hủy" && d.NgayDat.Value.Month == thang).Sum(d => d.TongTien.Value);
            ViewBag.doanhThuThangDuTinh = doanhThuThangDuTinh;
            //Số lượng sách đã bán ra trong tháng
            int slSachBanTrongThang = db.CHITIETDONHANGs.Where(d => d.DONHANG.TrangThai == "Đã hoàn thành" && d.DONHANG.NgayDat.Value.Month == thang).Sum(d => d.SL.Value);
            ViewBag.slSachBanTrongThang = slSachBanTrongThang;
            //số lượng khách hàng
            int slKH = db.KHACHHANGs.Where(k=>k.XacThuc==true).Count();
            ViewBag.slKH = slKH;
            return View();
        }


    }
}