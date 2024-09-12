using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace mağazastoktakip.Controllers
{
    [Authorize]
    public class MusteriController : Controller
    {
        // GET: Musteri
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index(int page = 1)
        {
            var musteriliste = db.TBLMusteri.Where(x=>x.durum==true).ToList().ToPagedList(page, 5);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri( TBLMusteri  p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.TBLMusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(TBLMusteri p)
        {
            var musteribul=db.TBLMusteri.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri=db.TBLMusteri.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult MusteriGuncelle(TBLMusteri p)
        {
            var mt = db.TBLMusteri.Find(p.id);
            mt.ad=p.ad;
            mt.soyad=p.soyad;
            mt.şehir=p.şehir;
            mt.bakiye=p.bakiye;
            db.SaveChanges();   
            return RedirectToAction("Index");
        }
    }
}