using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MiniBook.Models;

namespace MiniBook.Controllers
{
    public class CustomerController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: Customer
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.KHACHHANGs.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKhachHang,Ten,DiaChi,SDT,Mail,TenDangNhap,MatKhau,NgaySinh,GioiTinh,XacThuc")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACHHANG);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKhachHang,Ten,DiaChi,SDT,Mail,TenDangNhap,MatKhau,NgaySinh,GioiTinh,XacThuc")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "IDKhachHang,Ten,DiaChi,SDT,Mail,TenDangNhap,MatKhau,NgaySinh,GioiTinh,XacThuc")] KHACHHANG cus)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cus.Ten))
                    ModelState.AddModelError(String.Empty, "Không được để trống");
                if (string.IsNullOrEmpty(cus.TenDangNhap))
                    ModelState.AddModelError(String.Empty, "Không được để trống");
                if (string.IsNullOrEmpty(cus.MatKhau))
                    ModelState.AddModelError(String.Empty, "Không được để trống");
                if (string.IsNullOrEmpty(cus.Mail))
                    ModelState.AddModelError(String.Empty, "Không được để trống");
                if (string.IsNullOrEmpty(cus.SDT))
                    ModelState.AddModelError(String.Empty, "Không được để trống");
                if (string.IsNullOrEmpty(cus.DiaChi))
                    ModelState.AddModelError(String.Empty, "Không được để trống");

                var cusdb = db.KHACHHANGs.FirstOrDefault(c => c.TenDangNhap == cus.TenDangNhap);
                if (cusdb != null)
                {
                    ModelState.AddModelError(String.Empty, "tên đăng nhập đã được sữ dụng. Vui lòng chọn tên đăng nhập khác!");
                }
                if (ModelState.IsValid)
                {
                    cus.XacThuc = false;
                    db.KHACHHANGs.Add(cus);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(KHACHHANG cus)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra có admin này hay chưa
                var cusdb = db.KHACHHANGs.FirstOrDefault(c => c.TenDangNhap == cus.TenDangNhap && c.MatKhau == cus.MatKhau);
                if (cusdb == null)
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                else
                {
                    Session["IDCus"] = cusdb.IDKhachHang;
                    Session["CusName"] = cusdb.Ten;
                    Session["TaiKhoan"] = cusdb;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("Index", "MBook");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        // GET: Customer/ResetPassword
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        // POST: Customer/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Vui lòng nhập email.");
                return View();
            }

            var customer = db.KHACHHANGs.FirstOrDefault(c => c.Mail == email);
            if (customer == null)
            {
                ModelState.AddModelError("", "Email không tồn tại trên hệ thống. Vui lòng nhập lại");
                return View();
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập mật khẩu.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp.");
                return View();
            }

            customer.MatKhau = newPassword;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();

            TempData["Message"] = "Mật khẩu của bạn đã được thay đổi thành công. Vui lòng đăng nhập lại.";
            return RedirectToAction("Login");
        }
    }
}
