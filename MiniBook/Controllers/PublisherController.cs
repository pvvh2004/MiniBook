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
    public class PublisherController : Controller
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        // GET: Publisher
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.NXBs.ToList());
        }

        // GET: Publisher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publisher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNXB,SDT,Mail,DiaChi,TenNXB")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.NXBs.Add(nXB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nXB);
        }

        // GET: Publisher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: Publisher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNXB,SDT,Mail,DiaChi,TenNXB")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nXB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nXB);
        }

        // GET: Publisher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NXB nXB = db.NXBs.Find(id);
            db.NXBs.Remove(nXB);
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
