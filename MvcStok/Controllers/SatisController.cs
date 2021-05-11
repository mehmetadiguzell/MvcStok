using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SatisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(Satislar satis)
        {
            using (var context = new MvcStokDbEntities())
            {
                if (satis != null)
                {
                    context.Satislar.Add(satis);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }


    }
}