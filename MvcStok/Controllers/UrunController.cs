using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        public ActionResult Index()
        {
            using (var context = new MvcStokDbEntities())
            {
                var model = context.Urunler.Include("Kategoriler").ToList();
                if (model != null)
                {
                    return View(model);
                }
                return HttpNotFound();
            }
        }
        public ActionResult Ekle()
        {
            using (var context = new MvcStokDbEntities())
            {
                List<SelectListItem> result = (from i in context.Kategoriler.Where(c=>c.Status==true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.KategoriAdi,
                                                   Value = i.İd.ToString()
                                               }).ToList();
                ViewBag.res = result;

            }

            return View();

        }
        [HttpPost]
        public ActionResult Ekle(Urunler urun)
        {
            using (var context = new MvcStokDbEntities())
            {

                if (urun != null)
                {
                    var kategori = context.Kategoriler.Where(m => m.İd == urun.Kategoriler.İd).FirstOrDefault();
                    urun.Kategoriler = kategori;
                    context.Urunler.Add(urun);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }
        public ActionResult Sil(int id = 0)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Urunler.Find(id);
                if (result != null)
                {
                    context.Urunler.Remove(result);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }

        public ActionResult UrunFormu(int id = 0)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Urunler.Find(id);
                if (result != null)
                {
                    List<SelectListItem> items = (from i in context.Kategoriler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.KategoriAdi,
                                                      Value = i.İd.ToString()
                                                  }).ToList();
                    ViewBag.res = items;


                    return View("UrunFormu", result);
                }
                return HttpNotFound();
            }
        }

        public ActionResult Guncelle(Urunler urun)
        {
            using (var context = new MvcStokDbEntities())
            {
                var result = context.Urunler.Find(urun.İd);
                if (result != null)
                {
                    result.UrunAdi = urun.UrunAdi;
                    result.Marka = urun.Marka;
                    result.Stok = urun.Stok;
                    result.Fiyat = urun.Fiyat;
                    var kategori = context.Kategoriler.Where(m => m.İd == urun.Kategoriler.İd).FirstOrDefault();
                    result.KategoriId = kategori.İd;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

        }
    }
}