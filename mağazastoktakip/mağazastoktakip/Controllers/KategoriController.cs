using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;

namespace mağazastoktakip.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index()
        {
            var kategoriler = db.TBLKategori.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult yenikategori()
        {

            return View();
        }
        [HttpPost]
        public ActionResult yenikategori(TBLKategori p)
        {
            db.TBLKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult sil(int id)
        {
            var sil = db.TBLKategori.Find(id);
            db.TBLKategori.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKategori.Find(id);
           
            return View("KategoriGetir",ktgr);
        }
        public ActionResult KategoriGuncelle(TBLKategori p)
        {
            var ktg=db.TBLKategori.Find(p.id);
            ktg.ad=p.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}