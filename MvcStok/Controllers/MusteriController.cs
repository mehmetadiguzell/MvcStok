using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        public ActionResult Index()
        {
            using (var context = new MvcStokDbEntities())
            {
                var model = context.Musteriler.ToList();
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
        public ActionResult Ekle(Musteriler musteri)
        {
            using (var context = new MvcStokDbEntities())
            {
                if (ModelState.IsValid)
                {
                    if (musteri != null)
                    {
                        context.Musteriler.Add(musteri);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
        }

        public ActionResult Sil(int id = 0)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Musteriler.Find(id);
                if (result != null)
                {
                    context.Musteriler.Remove(result);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }

        public ActionResult MusteriFormu(int id = 0)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Musteriler.Find(id);
                if (result != null)
                {
                    return View("MusteriFormu", result);
                }
                return HttpNotFound();
            }
        }

        public ActionResult Guncelle(Musteriler musteri)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Musteriler.Find(musteri.İd);
                if (result != null)
                {
                    result.MusteriAdi = musteri.MusteriAdi;
                    result.MusteriSoyad = musteri.MusteriSoyad;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }
    }
}