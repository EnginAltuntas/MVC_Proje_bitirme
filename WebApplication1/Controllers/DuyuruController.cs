using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class DuyuruController : Controller
    {
        Model1 ctx = new Model1();
        private readonly IBlobStorageRepository repo;
        public DuyuruController(IBlobStorageRepository _repo)
        {
            this.repo = _repo;
        }
        public ActionResult Duyuru_goster()
        {
            List<Duyuru> d = ctx.Duyuru.ToList();
            ViewBag.duyurular = d;
            ViewBag.sinif__id = TempData["sinif_id"];
            ViewBag.sinif_adi = TempData["sinif_adi"];
            ViewBag.uye_sayi = TempData["uye_sayi"];



            return View();
        }

        public ActionResult Duyuru_g(int id)
        {
            List<Duyuru> du = ctx.Duyuru.ToList();
            foreach (Duyuru d in du)
            {
                if (d.sinif_id == id)
                {
                    TempData["sinif_adi"] = d.Sinif.sinif_adi;
                    TempData["uye_sayi"] = ctx.Kontrol_sinif.Count(x => x.sinif_id == d.sinif_id);
                    break;
                }
                    
            }
            TempData["sinif_id"] = id;
            return RedirectToAction("Duyuru_goster");
        }

        [HttpPost]
        public ActionResult Duyuru_ekle(string baslik,string bildiri,HttpPostedFileBase dosya,int sinif_id,string sinif_adi)
        {
            Duyuru d = new Duyuru();
            d.baslik = baslik;
            d.bildiri = bildiri;
            d.sinif_id = sinif_id;
            bool isUploaded = repo.UploadBlob(dosya,1,0,0);
            if (isUploaded == true)
            {
                d.dosya = BlobStorageRepository._isim;
                ctx.Duyuru.Add(d);
                ctx.SaveChanges();
                TempData["sinif_id"] = sinif_id;
                TempData["sinif_adi"] = sinif_adi;
                ViewBag.sonuc = "Dosya Yükleme Başarılı";
                return RedirectToAction("Duyuru_goster");
            }
            else
            {
                ctx.Duyuru.Add(d);
                ctx.SaveChanges();
                TempData["sinif_id"] = sinif_id;
                TempData["sinif_adi"] = sinif_adi;
                ViewBag.sonuc = "Dosya Seçilmedi";
                return RedirectToAction("Duyuru_goster");
            }
                   
        }

        public ActionResult duyuruBilgi(int id)
        {
            List<Kontrol_odev> k = ctx.Kontrol_odev.ToList();
            ViewBag.kontrolOdev = k;
            ViewBag.qq = ctx.Kontrol_odev.Count(x => x.duyuru_id == id);
            ViewBag.did = id;
            return View();
        } //burası 2 kere çalışıyor 

        public ActionResult kisilerListe(int id) //sınıf_id
        {
            ViewBag.kisiSayi = ctx.Kontrol_sinif.Count(x => x.sinif_id == id);
            ViewBag.ks = ctx.Kontrol_sinif.ToList();
            ViewBag.sid = id;


            return View();
        }

        public void duyuruKaldir(int id)
        {
            Duyuru d = ctx.Duyuru.FirstOrDefault(x => x.duyuru_id == id);
            ctx.Duyuru.Remove(d);
            ctx.SaveChanges();
        }





    }
}