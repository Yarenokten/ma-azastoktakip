using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mağazastoktakip.Models.Entity;

namespace mağazastoktakip.Controllers
{
    [Authorize]

    public class SatislarController : Controller
    {
        DBMvcStokEntities db=new DBMvcStokEntities();
        // GET: Satislar
        public ActionResult Index()
        {
            var satislar=db.TBLSatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from x in db.TBLUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();  
            ViewBag.drop1= urun;
            List<SelectListItem> personel = (from x in db.TBLPersonel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.ad+" " + x.soyad,
                                                 Value = x.id.ToString()
                                             }).ToList();
            ViewBag.drop2 = personel;
            List<SelectListItem> musteri = (from x in db.TBLMusteri.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.ad +" "+ x.soyad,
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.drop3=musteri;
           
           return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBLSatislar p)
        {
            var urun=db.TBLUrunler.Where(x=>x.id==p.TBLUrunler.id).FirstOrDefault();
            var musteri=db.TBLMusteri.Where(x=>x.id==p.TBLMusteri.id).FirstOrDefault();
            var personel=db.TBLPersonel.Where(x=>x.id==p.TBLPersonel.id).FirstOrDefault();
            p.TBLUrunler = urun;
            p.TBLMusteri = musteri;
            p.TBLPersonel = personel;
            p.tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLSatislar.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        

    }
}