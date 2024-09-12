using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;
using System.Web.Security;

namespace mağazastoktakip.Controllers
{
    public class GirisController : Controller
    {
        // GET: Giris
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(TBLAdmin t)
        {
            var bilgiler = db.TBLAdmin.FirstOrDefault(x => x.kullanici == t.kullanici && x.sifre == t.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult yenisifre()
        {
            return View();
        }
        [HttpPost]
        public ActionResult yenisifre(TBLAdmin p)
        {
            if (ModelState.IsValid)
            {
                var admin = db.TBLAdmin.SingleOrDefault(x => x.kullanici == p.kullanici);

                if (admin != null)
                {
                    
                    admin.sifre = p.sifre;
                    db.SaveChanges();

                    ViewBag.Success = true;  
                    return View("Index"); 
                }
                else
                {
                    ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                    return View("Index");
                }
            }
            return View("Index");
        }
    }
}