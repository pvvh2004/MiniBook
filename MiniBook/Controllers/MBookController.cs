using Microsoft.Ajax.Utilities;
using MiniBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace MiniBook.Controllers
{
    public class MBookController : Controller
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();
        // GET: MBook
        private List<SACH> TopSaleBooks(int soluong)
        {
            return db.SACHes.OrderByDescending(sach =>sach.SLBan).Take(soluong).ToList();
        }

        public ActionResult Index()
        {
            var sp = TopSaleBooks(4);
            return View(sp);
        }
        public ActionResult AllBook(string searchString, int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var sachs = db.SACHes.OrderByDescending(sach => sach.NgayPhatHanh).ToList();
            if(searchString != null)
            {
                sachs = db.SACHes.Where(s =>s.TenSach.Contains(searchString)).ToList();

            }

            return View(sachs.ToPagedList(pageNum,pageSize));
        }
        public ActionResult GetCategories ()
        {
            var c =db.THELOAIs.ToList();
            return PartialView(c); 
        }
        public ActionResult BookForCate(int id, int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var books =db.SACHes.Where( b=> b.IDTheLoai == id).ToList();
            return View("AllBook",books.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Details (int id)
        {
            var book = db.SACHes.FirstOrDefault(b => b.IDSach == id);
            return View(book);
        }
    }
}