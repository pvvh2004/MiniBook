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
    public class AuthorController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: Author
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.TACGIAs.ToList());
        }

        // GET: Author/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTG,Ten,DiaChi,Mail,GioiThieu")] TACGIA tACGIA)
        {
            if (ModelState.IsValid)
            {
                db.TACGIAs.Add(tACGIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tACGIA);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTG,Ten,DiaChi,Mail,GioiThieu")] TACGIA tACGIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tACGIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tACGIA);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tACGIA = db.TACGIAs.Find(id);
            if (tACGIA == null)
            {
                return HttpNotFound();
            }
            return View(tACGIA);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TACGIA tACGIA = db.TACGIAs.Find(id);
            db.TACGIAs.Remove(tACGIA);
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
