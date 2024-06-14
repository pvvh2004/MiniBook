using MiniBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MiniBook.Controllers
{
    public class GioHangController : Controller
    {
        BOOKSTOREEntities db=new BOOKSTOREEntities();
        public List<MatHangMua> LayGioHang()
        {
            List<MatHangMua> gioHang = Session["GioHang"] as List<MatHangMua>;
            if (gioHang == null)
            {
                gioHang =new List<MatHangMua>();
                Session["GioHang"]=gioHang;
            }
            return gioHang;
        }
        public ActionResult ThemSanPhamVaoGio(int IDSach)
        {
            List<MatHangMua> gioHang = LayGioHang();
            MatHangMua sanPham = gioHang.FirstOrDefault(s => s.IDSach == IDSach);
            if (sanPham == null)
            {
                sanPham = new MatHangMua(IDSach);
                gioHang.Add(sanPham);
            }
            else
            {
                sanPham.SoLuong++;
            }
            return RedirectToAction("AllBook", "MBook", new { id = IDSach });

        }
        private int TinhTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();

            if (gioHang != null)
            {
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            }
            return tongSL;
        }
        private double TinhTongTien()
        {
            double tongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
            {
                tongTien = gioHang.Sum(sp => sp.ThanhTien());
            }
            return tongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("AllBook", "MBook");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult XoaMatHang(int IDSach)
        {
            List<MatHangMua> gioHang = LayGioHang();

            var sanPham = gioHang.FirstOrDefault(s => s.IDSach == IDSach);
            if (sanPham != null)
            {
                gioHang.RemoveAll(s => s.IDSach == IDSach);
                return RedirectToAction("HienThiGioHang");
            }
            if (gioHang.Count == 0)
            {
                return RedirectToAction("AllBook", "MBook");
            }
            return RedirectToAction("HienThiGioHang");
        }

        public ActionResult CapNhatMatHang(int IDSach, int SoLuong)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanPham = gioHang.FirstOrDefault(s => s.IDSach == IDSach);

            if (sanPham != null)
            {
                sanPham.SoLuong = SoLuong;
            }
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult DatHang()
        {
            if (Session["CusName"] == null)
                return RedirectToAction("Login", "Customer");
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("AllBook", "MBook");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult UseVoucher(string Ma)
        {
            var vou = db.VOUCHERs.FirstOrDefault(v=>v.Ma== Ma);
            if (vou != null && vou.SLSD>0)
            {
                ViewBag.Voucher = vou.TienGiam;
                double tien= TinhTienSauVoucher(double.Parse(vou.TienGiam.ToString()));
                ViewBag.TongTien = tien;
                Session["Voucher"] = 1;
            }
            return RedirectToAction("DatHang");
        }
        private double TinhTienSauVoucher(double TienGiam)
        {
             return TinhTongTien() - TienGiam;
        }
        public ActionResult DongYDatHang()
        {
            KHACHHANG khach = Session["TaiKhoan"] as KHACHHANG;
            List<MatHangMua> giohang = LayGioHang();

            DONHANG donHang = new DONHANG();
            donHang.IDKhachHang = khach.IDKhachHang;
            donHang.NgayDat = DateTime.Now;
            donHang.TongTien = (float)TinhTongTien();
            donHang.TrangThai = "Chờ xác nhận";
            donHang.TenNguoiNhan = khach.Ten;
            donHang.DiaChiNhan = khach.DiaChi;
            donHang.SDTNhan = khach.SDT;
            donHang.HTThanhToan ="Chưa xác định";
            donHang.HTGiaoHang = "VNEX";
            db.DONHANGs.Add(donHang);
            db.SaveChanges();
            foreach (var sanpham in giohang)
            {
                CHITIETDONHANG chiTiet = new CHITIETDONHANG();
                
                chiTiet.IDDonHang = donHang.IDDonHang;
                chiTiet.IDSach = sanpham.IDSach;
                chiTiet.SL = sanpham.SoLuong;
                chiTiet.DonGia = (float)sanpham.GiaBan;
                chiTiet.ThanhTien = chiTiet.SL * chiTiet.DonGia;
                db.CHITIETDONHANGs.Add(chiTiet);
                //cập nhật sách
                SACH sach = db.SACHes.Find(chiTiet.IDSach);
                sach.SLBan = sach.SLBan +sanpham.SoLuong;
                sach.SoLuongKho = sach.SoLuongKho - sanpham.SoLuong;
                db.SaveChanges();
            }
            db.SaveChanges();

            Session["GioHang"] = null;
            return RedirectToAction("HoanThanhDonHang");
        }

        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
    }
}