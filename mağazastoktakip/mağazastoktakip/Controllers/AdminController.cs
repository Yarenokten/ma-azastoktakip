using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;

namespace mağazastoktakip.Controllers
{
    [Authorize]

    public class AdminController : Controller
    {
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index()
        {
            var admin= db.TBLAdmin.ToList();
            return View(admin);
            
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLAdmin p)
        {
            if (ModelState.IsValid)
            {
                db.TBLAdmin.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Eğer form verileri geçerli değilse, Index görünümünü tekrar göster
            return View("Index");
        }
    }
}