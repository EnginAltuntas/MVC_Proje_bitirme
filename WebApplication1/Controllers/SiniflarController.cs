using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class SiniflarController : Controller
    {
        private readonly IBlobStorageRepository repo;

        public SiniflarController(IBlobStorageRepository _repo)
        {
            this.repo = _repo;
        }

        Model1 ctx = new Model1();
        ArrayList sinifs = new ArrayList();
        [AllowAnonymous]
        public ActionResult ogrenci()
        {
            List<Kullanicilar> kullanicilar = ctx.Kullanicilar.ToList();

            foreach (Kullanicilar k in kullanicilar)
            {
                if(User.Identity.Name==k.kullanici_adi)
                {
                    int kId = k.kullanici_id;
                    List<Ogrenci> ogrenciler = ctx.Ogrenci.ToList();
                    foreach (Ogrenci o in ogrenciler)
                    {
                        if(kId==o.kullanici_id) //öğrenci_id ye ulaşıldı
                        {
                            List<Kontrol_sinif> k_sinif = ctx.Kontrol_sinif.ToList();
                            ViewBag.bbb = k_sinif;
                            foreach (Kontrol_sinif ks in k_sinif)
                            {
                                if(ks.ogrenci_id==o.ogrenci_id)
                                {
                                    ViewBag.ccc = 1;
                                }
                            }
                        }
                    }

                }
            }
            ViewBag.sinifs = sinifs;
              
            return View();
        }
        [AllowAnonymous]
        public ActionResult sinif_goster()
        {
            ViewBag.du = TempData["du"];
            ViewBag.sinif_id = TempData["sinif_id"];
            ViewBag.sinif_adi = TempData["sinif_adi"];
            ViewBag.oid = TempData["oid"];
            ViewBag.kont = ctx.Kontrol_odev.ToList();
            ViewBag.odev = ctx.Odevler.ToList();
            ViewBag.iii = 0;
            var blobVM = repo.GetBlobs();
            return View(blobVM);
        }

        public ActionResult goster1()
        {


   //         System.Threading.Thread.Sleep(1500);
            return View();
        }

        [AllowAnonymous]

        public ActionResult goster(int id)
        {
            List<Duyuru> du = ctx.Duyuru.ToList();
            List<Ogrenci> ogrenciler = ctx.Ogrenci.ToList();
            foreach (Ogrenci o in ogrenciler)
            {
                if (o.Kullanicilar.kullanici_adi == User.Identity.Name)
                {
                    TempData["oid"] = o.ogrenci_id;
                    break;
                }
            }
            foreach (Duyuru d in du)
            {
                if (d.sinif_id == id)
                {
                    TempData["sinif_adi"] = d.Sinif.sinif_adi;
                    break;
                }
            }

            TempData["du"] = du;
            TempData["sinif_id"] = id;

            return RedirectToAction("sinif_goster");
        }

        public ActionResult kisilerListe(int id) //sınıf_id
        {
            ViewBag.kisiSayi = ctx.Kontrol_sinif.Count(x => x.sinif_id == id);
            ViewBag.ks = ctx.Kontrol_sinif.ToList();
            ViewBag.sid = id;


            return View();
        }


        public void siniftancik(int id)
        {
            Kontrol_sinif ks = ctx.Kontrol_sinif.FirstOrDefault(x => x.sinif_id == id);
            ctx.Kontrol_sinif.Remove(ks);
            ctx.SaveChanges();
            
        }

        public void sinifSil(int id)
        {
            int sayi = ctx.Duyuru.Count(x => x.sinif_id == id);
            for (int i = 0; i < sayi; i++)
            {
                Duyuru d = ctx.Duyuru.FirstOrDefault(x => x.sinif_id == id);
                ctx.Duyuru.Remove(d);
                ctx.SaveChanges();
            }
            int sayi2 = ctx.Kontrol_sinif.Count(x => x.sinif_id == id);
            for (int i = 0; i < sayi2; i++)
            {
                Kontrol_sinif k = ctx.Kontrol_sinif.FirstOrDefault(x => x.sinif_id == id);
                ctx.Kontrol_sinif.Remove(k);
                ctx.SaveChanges();
            }
            
            Sinif s = ctx.Sinif.FirstOrDefault(x => x.sinif_id == id);
            Yonetici y = ctx.Yonetici.FirstOrDefault(x=>x.yonetici_id==s.yonetici_id);
            ctx.Yonetici.Remove(y);
            ctx.SaveChanges();
            ctx.Sinif.Remove(s);
            ctx.SaveChanges();

        }

        public void odevKaldir(int id)
        {
            Odevler odevler = ctx.Odevler.FirstOrDefault(x => x.odev_id == id);
            ctx.Odevler.Remove(odevler);
            ctx.SaveChanges();

            Kontrol_odev ko = ctx.Kontrol_odev.FirstOrDefault(x => x.odev_id == id);
            ctx.Kontrol_odev.Remove(ko);
            ctx.SaveChanges();

        }

        public bool odevEkleKontrol(int kime,int duyuru)
        {
            List<Odevler> odevler = ctx.Odevler.ToList();
            foreach (Odevler od in odevler)
            {
                if (od.ogrenci_id == kime)
                {
                    List<Kontrol_odev> kontrol_odev = ctx.Kontrol_odev.ToList();
                    foreach (Kontrol_odev kk in kontrol_odev)
                    {
                        if (kk.duyuru_id == duyuru && od.odev_id == kk.odev_id)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }



    }
}