using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;

namespace mağazastoktakip.Controllers
{
    [Authorize]

    public class UrunController : Controller
    {
        // GET: Urun
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index(string p)
        {
            //var urunler = db.TBLUrunler.Where(x=>x.durum==true).ToList();
            var urunler = db.TBLUrunler.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler=urunler.Where(x=>x.ad.Contains(p) &&  x.durum == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.TBLKategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLUrunler p)
        {
            var ktgr = db.TBLKategori.Where(x => x.id == p.TBLKategori.id).FirstOrDefault();
            p.TBLKategori = ktgr;
            db.TBLUrunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.TBLKategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            var urun=db.TBLUrunler.Find(id);
            ViewBag.dropurun = kat;
            return View("UrunGetir",urun);
        }
        public ActionResult UrunGuncelle(TBLUrunler p)
        {
            var urun = db.TBLUrunler.Find(p.id);
            urun.ad=p.ad;
            urun.marka=p.marka;
            urun.stok=p.stok;
            urun.alisfiyat=p.alisfiyat;
            urun.satisfiyat=p.satisfiyat;
            var ktg=db.TBLKategori.Where(x=>x.id==p.TBLKategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TBLUrunler p)
        {
            var urunbul = db.TBLUrunler.Find(p.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}