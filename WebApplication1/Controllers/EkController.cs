using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class EkController : Controller
    {
        Model1 ctx = new Model1();
        public ActionResult Profil()
        {
            ViewBag.yoneticiSayi = ctx.Yonetici.Count(x => x.Kullanicilar.kullanici_adi == User.Identity.Name);
            List<Sinif> siniflar = ctx.Sinif.ToList();
            ViewBag.siniflar = siniflar;

            List<Kontrol_sinif> kontrol_sinif = ctx.Kontrol_sinif.ToList();
            ViewBag.kontrol_sinif = kontrol_sinif;

            ViewBag.duyuruSayi = ctx.Duyuru.Count(x => x.Sinif.Yonetici.Kullanicilar.kullanici_adi == User.Identity.Name);

            ViewBag.ogrenciSayi = ctx.Kontrol_sinif.Count(x => x.Ogrenci.Kullanicilar.kullanici_adi == User.Identity.Name);

            ViewBag.odevSayi = ctx.Odevler.Count(x => x.Ogrenci.Kullanicilar.kullanici_adi == User.Identity.Name);

            return View();
        }

        public ActionResult Ayarlar()
        {
            return View();
        }

        
        public ActionResult mailDegis()
        {
            ViewBag.durum = TempData["durum"];
            return View();
        }

        [HttpPost]
        public ActionResult mailKontrol(string eski_mail, string yeni_mail1, string yeni_mail2)
        {
            if (yeni_mail1 != yeni_mail2)
            {
                TempData["durum"] = 1;
                return RedirectToAction("mailDegis");
            }
            else
            {
                Kullanicilar k = ctx.Kullanicilar.FirstOrDefault(x => x.kullanici_adi == User.Identity.Name);
                if (k.email != eski_mail)
                {
                    TempData["durum"] = 2;
                    return RedirectToAction("mailDegis");
                }
                else
                {
                    var kullanicilar = ctx.Kullanicilar.Find(k.kullanici_id);
                    kullanicilar.email = yeni_mail1;
                    ctx.SaveChanges();
                }
            }

            TempData["durum"] = 3;
            return RedirectToAction("mailDegis");
        }

        public ActionResult sifreDegis()
        {
            ViewBag.durum = TempData["durum"];
            return View();
        }

        [HttpPost]
        public ActionResult sifreKontrol(string eski_sifre, string yeni_sifre1, string yeni_sifre2 )
        {
           if(yeni_sifre1 != yeni_sifre2)
            {
                TempData["durum"] = 1;
                return RedirectToAction("sifreDegis");
            }
            else
            {
                Kullanicilar k = ctx.Kullanicilar.FirstOrDefault(x=> x.kullanici_adi==User.Identity.Name);
                if(k.sifre != eski_sifre)
                {
                    TempData["durum"] = 2;
                    return RedirectToAction("sifreDegis");
                }
                else
                {
                    var kullanicilar = ctx.Kullanicilar.Find(k.kullanici_id);
                    kullanicilar.sifre = yeni_sifre1;
                    ctx.SaveChanges();
                }
            }

            TempData["durum"] = 3;
            return RedirectToAction("sifreDegis");
        }

    }
}