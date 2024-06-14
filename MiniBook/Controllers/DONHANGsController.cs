using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniBook.Models;

namespace MiniBook.Controllers
{
    public class DONHANGsController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: DONHANGs
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            var dONHANGs = db.DONHANGs.OrderByDescending(d => d.NgayDat).Include(d => d.KHACHHANG).Include(d => d.VOUCHER1);
            return View(dONHANGs.ToList());
        }
        public ActionResult LichSuDonHang(int id)
        {
            var dsDon = db.DONHANGs.Where(d => d.IDKhachHang == id).ToList();
            return View(dsDon);
        }
        public ActionResult DanhSachSanPham(int id)
        {
            
            var dss = db.CHITIETDONHANGs.Where(d => d.IDDonHang == id).ToList();
            return View(dss);
        }
        // GET: DONHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: DONHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhachHang = new SelectList(db.KHACHHANGs, "IDKhachHang", "Ten", dONHANG.IDKhachHang);
            ViewBag.Voucher = new SelectList(db.VOUCHERs, "Ma", "Ma", dONHANG.Voucher);
            return View(dONHANG);
        }

        // POST: DONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDonHang,NgayDat,TongTien,NgayGiao,DiaChiNhan,SDTNhan,TenNguoiNhan,NgayGuiHang,HTGiaoHang,HTThanhToan,TrangThai,IDKhachHang,Voucher")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhachHang = new SelectList(db.KHACHHANGs, "IDKhachHang", "Ten", dONHANG.IDKhachHang);
            ViewBag.Voucher = new SelectList(db.VOUCHERs, "Ma", "Ma", dONHANG.Voucher);
            return View(dONHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ChiTietDonHang(int id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            var ctDonHang = db.CHITIETDONHANGs.Where(ct => ct.IDDonHang == id).ToList();
            return View(ctDonHang);
        }
    }
}
