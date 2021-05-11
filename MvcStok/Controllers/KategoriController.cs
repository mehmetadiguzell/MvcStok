using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        public ActionResult Index(int page=1)
        {
            using (var context = new MvcStokDbEntities())
            {
                var model = context.Kategoriler.ToList().ToPagedList(page, 3);
                if (model != null)
                {
                    return View(model);
                }
                return HttpNotFound();
            }

        }

        public ActionResult Ekle()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler kategori)
        {
            using (var context = new MvcStokDbEntities())
            {
                if (ModelState.IsValid)
                {
                    if (kategori != null)
                    {
                        context.Kategoriler.Add(kategori);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }             
                return View();
            }

        }

        public ActionResult Sil(int id=0)
        {
            using (var context = new MvcStokDbEntities())
            {
               
                var kategori = context.Kategoriler.Find(id);
                if (kategori != null)
                {
                    kategori.Status = false;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }
        public ActionResult KategoriFormu(int id = 0)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Kategoriler.Find(id);
                if (result != null)
                {
                    return View("KategoriFormu",result);
                }
                return HttpNotFound();
            }
        }

        public ActionResult Guncelle(Kategoriler kategori)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Kategoriler.Find(kategori.İd);
                if (result != null)
                {
                    result.KategoriAdi = kategori.KategoriAdi;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }
    }
}