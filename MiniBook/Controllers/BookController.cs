using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniBook.Models;

namespace MiniBook.Controllers
{
    public class BookController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: Book
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            var sACHes = db.SACHes.Include(s => s.NXB).Include(s => s.THELOAI);
            return View(sACHes.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            ViewBag.IDNXB = new SelectList(db.NXBs, "IDNXB", "TenNXB");
            ViewBag.IDTheLoai = new SelectList(db.THELOAIs, "IDTheLoai", "TenTheLoai");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSach,TenSach,GiaBan,SoLuongKho,MoTa,AnhMinhHoa,NgayPhatHanh,SLXem,SLBan,IDNXB,IDTheLoai")] SACH sACH, HttpPostedFileBase AnhMinhHoa)
        {
            if (AnhMinhHoa!=null)
            {
                var fileName = Path.GetFileName(AnhMinhHoa.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Ảnh đã tồn tại";
                }
                else
                {
                    AnhMinhHoa.SaveAs(path);
                }
                if (ModelState.IsValid)
                {
                    sACH.AnhMinhHoa = fileName;
                    db.SACHes.Add(sACH);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IDNXB = new SelectList(db.NXBs, "IDNXB", "SDT", sACH.IDNXB);
                ViewBag.IDTheLoai = new SelectList(db.THELOAIs, "IDTheLoai", "TenTheLoai", sACH.IDTheLoai);
                return View(sACH);
            }
            ViewBag.ThongBao = "Vui lòng điền đầy đủ thông tin";
            return View();
            
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNXB = new SelectList(db.NXBs, "IDNXB", "SDT", sACH.IDNXB);
            ViewBag.IDTheLoai = new SelectList(db.THELOAIs, "IDTheLoai", "TenTheLoai", sACH.IDTheLoai);
            return View(sACH);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSach,TenSach,GiaBan,SoLuongKho,MoTa,AnhMinhHoa,NgayPhatHanh,SLXem,SLBan,IDNXB,IDTheLoai")] SACH sACH, HttpPostedFileBase AnhMinhHoa)
        {   
            if (ModelState.IsValid)
            {
                var sachDB = db.SACHes.FirstOrDefault(s => s.IDSach == sACH.IDSach);
                var fileName = Path.GetFileName(AnhMinhHoa.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                sACH.AnhMinhHoa = fileName;
                AnhMinhHoa.SaveAs(path);

                sachDB.TenSach = sACH.TenSach;
                sachDB.GiaBan = sACH.GiaBan;
                sachDB.SoLuongKho = sACH.SoLuongKho;
                sachDB.MoTa =sACH.MoTa;
                sachDB.AnhMinhHoa = sACH.AnhMinhHoa;
                sachDB.NgayPhatHanh = sACH.NgayPhatHanh;
                sachDB.SLXem=sACH.SLXem;
                sachDB.SLBan=sACH.SLBan;
                sachDB.IDNXB = sACH.IDNXB;
                sachDB.IDTheLoai = sACH.IDTheLoai;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNXB = new SelectList(db.NXBs, "IDNXB", "SDT", sACH.IDNXB);
            ViewBag.IDTheLoai = new SelectList(db.THELOAIs, "IDTheLoai", "TenTheLoai", sACH.IDTheLoai);
            return View(sACH);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH sACH = db.SACHes.Find(id);
            db.SACHes.Remove(sACH);
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
    }
}
