using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1;

namespace proje_layout_deneme.Controllers
{
    [Authorize] // Bu controllerın açılması için kullanıcının sisteme giriş yapması gerek
    // kaç tane controller varsa hepsine yazılmalıdır
    public class AnaSayfaController : Controller
    {
        public static int kullanicinin_idsi;

        Model1 ctx = new Model1();
        //   ProjeContext ctx = new ProjeContext();
        
        [AllowAnonymous]
        public ActionResult AnaSayfa()
        {

            List<Kullanicilar> kullanicilar = ctx.Kullanicilar.ToList();

            return View(kullanicilar);
        } // B

        public ActionResult OturumEkranı()
        {
            // sınıfadı, kurucu adı , üye sayısı

            return View();
        } //B

        [AllowAnonymous]
        public ActionResult HesapAc()
        {

            return View();
        }// B

        public ActionResult sınıfaKatıl()
        {
            if(TempData["alert"]!=null)
            {
                if (int.Parse(TempData["alert"].ToString()) == 1)
                {
                    ViewBag.bilgi = 1;
                }
                else if (int.Parse(TempData["alert"].ToString()) == 2)
                {
                    ViewBag.bilgi = 2;
                }
                else if (int.Parse(TempData["alert"].ToString()) == 3)
                {
                    ViewBag.bilgi = 3;
                }
                else if (int.Parse(TempData["alert"].ToString()) == 4)
                {
                    ViewBag.bilgi = 4;
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult SınıfKatıl(string kod)
        {
            bool sinifkatilimdenetim = SinifKatilimDenetim(kod);
            if(sinifkatilimdenetim == false)
            {
                TempData["alert"] = 4;
                return RedirectToAction("sınıfaKatıl");
            }

            int sınıf_kontrol = 0;
            List<Sinif> siniflar = ctx.Sinif.ToList();
            foreach (Sinif s in siniflar)
            {
                if(s.sinif_kodu==kod)
                {
                    sınıf_kontrol++;
                    int sinif_id = s.sinif_id;
                    List<Ogrenci> ogrenciler = ctx.Ogrenci.ToList();
                    if(ogrenciler.Count==0)
                    {
                        Ogrenci o = new Ogrenci();
                        o.kullanici_id = kullanicinin_idsi;
                        ctx.Ogrenci.Add(o);
                        ctx.SaveChanges();
                        Kontrol_sinif ko = new Kontrol_sinif();
                        ko.ogrenci_id = o.ogrenci_id;
                        ko.sinif_id = s.sinif_id;
                        ctx.Kontrol_sinif.Add(ko);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        int kontrol = 0;
                        foreach (Ogrenci o1 in ogrenciler)
                        {
                            if (o1.kullanici_id == kullanicinin_idsi)
                            {
                                Kontrol_sinif ks = new Kontrol_sinif();
                                List<Kontrol_sinif> kont = ctx.Kontrol_sinif.ToList();
                                foreach (Kontrol_sinif k in kont)
                                {
                                    if(k.ogrenci_id==o1.ogrenci_id && s.sinif_id==k.sinif_id) //aynı sınıfa 2 defa katılma
                                    {

                                        sınıf_kontrol = 61;
                                        break;
                                    }
                                }
                                if(sınıf_kontrol==61)
                                {
                                    kontrol++;
                                    break;
                                }
                                else
                                {
                                    ks.sinif_id = sinif_id;
                                    ks.ogrenci_id = o1.ogrenci_id;
                                    ctx.Kontrol_sinif.Add(ks);
                                    ctx.SaveChanges();
                                    kontrol++;
                                }
                                
                            }
                            
                        }
                        if(kontrol==0)
                        {
                            Ogrenci o = new Ogrenci();
                            o.kullanici_id = kullanicinin_idsi;
                            ctx.Ogrenci.Add(o);
                            ctx.SaveChanges();
                            Kontrol_sinif kos = new Kontrol_sinif();
                            kos.ogrenci_id = o.ogrenci_id;
                            kos.sinif_id = s.sinif_id;
                            ctx.Kontrol_sinif.Add(kos);
                            ctx.SaveChanges();
                        }
                    }
                    
                }

            }
            if(sınıf_kontrol==0)
            {
                TempData["alert"] = 1;
                //sınıf kodu yanlış girildi.
            }
            else if(sınıf_kontrol==61)
            {
                TempData["alert"] = 3; // sınıfa daha önce katılım sağlandı
            }
            else
            {
                TempData["alert"] = 2; // başarılı
                ViewBag.bilgi = 2;
            }

            return RedirectToAction("sınıfaKatıl");
        }


        public ActionResult SınıfOlustur()
        {
            List<Kullanicilar> ku = ctx.Kullanicilar.ToList();
            Kullanicilar k = new Kullanicilar();
            foreach (Kullanicilar i in ku)
            {
                if (i.kullanici_adi == User.Identity.Name)
                    ViewBag.kullanici_id = i.kullanici_id;
            }
            int deneme = ViewBag.kullanici_id;
            return View();

        }
        public ActionResult Sinif()
        {
            List<Sinif> s = ctx.Sinif.ToList();
            ViewBag.Sinif_Bilgiler = s;
            foreach (Sinif sinif in s)
            {
                if(sinif.Yonetici.Kullanicilar.kullanici_adi==User.Identity.Name)
                {
                    ViewBag.aaa = 1;
                }
            }

            return View();
        }

        public ActionResult Sinifim()
        {
            List<Sinif> s = ctx.Sinif.ToList();
            ViewBag.Sinif_Bilgiler = s;
            //aaaaa
            var sinifkodu = TempData["sk"];
            ViewBag.sinifkodu = sinifkodu;
            return View();
        }
        public ActionResult SinifGetir(int id)
        {
            List<Sinif> s = ctx.Sinif.ToList();
            ViewBag.Sinif_Bilgi = s;
            var sinif = ctx.Sinif.Find(id);
            ViewBag.sinif = sinif.sinif_id;
            return View();
        }

        [HttpPost]
        ////////////////////////////
        public ActionResult SinifEkle(Sinif s)
        {
            Yonetici y = new Yonetici();

            List<Kullanicilar> ku = ctx.Kullanicilar.ToList();
            foreach (Kullanicilar i in ku)
            {
                if (i.kullanici_adi == User.Identity.Name)
                {
                    y.kullanici_id = i.kullanici_id;
                    //s.Yonetici.yonetici_id
                    s.yonetici_id = y.yonetici_id;
                }
            }
            //if (!ModelState.IsValid)
            //{

            //    return View("SınıfOlustur");
            //}

            s.sinif_kodu = CreatePassword(8);
            

            TempData["sk"] = s.sinif_kodu; //sinif kodunu sinifim actionuna taşır

            ctx.Yonetici.Add(y);
            ctx.Sinif.Add(s);
            ctx.SaveChanges();

            return RedirectToAction("Sinifim");
        }


        [HttpPost]

        public ActionResult KullaniciEkle(string ad, string soyad, string email, string sehir, string cinsiyet, string kullanici_adi, string sifre)
        {

            Kullanicilar k = new Kullanicilar();
            k.ad = ad;
            k.soyad = soyad;
            k.email = email;
            k.sehir = sehir;
            k.cinsiyet = cinsiyet;
            k.kullanici_adi = kullanici_adi;
            k.sifre = sifre;

            ctx.Kullanicilar.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("AnaSayfa");
        }
        // Ba

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AnaSayfa(string kullanici_adi, string sifre, string Hatirla)
        {
            List<Kullanicilar> kullanicilar1 = ctx.Kullanicilar.ToList();
            foreach (Kullanicilar h in kullanicilar1)
            {
                
                //  bool sonuc = Membership.ValidateUser(h.kullanici_adi, h.sifre);    foreach le aynı işi yapar
                if (h.kullanici_adi == kullanici_adi && h.sifre == sifre)
                {
                    if (Hatirla == "on")
                        FormsAuthentication.RedirectFromLoginPage(h.kullanici_adi, true);
                    else
                        FormsAuthentication.RedirectFromLoginPage(h.kullanici_adi, false);
                    return RedirectToAction("OturumEkranı", "AnaSayfa");
                }
                /*        else
                        {
                            ViewBag.Mesaj = "Kullanıcı adı veya şifre hatalı!";
                            return RedirectToAction("AnaSayfa", "AnaSayfa");
                        }*/
            }
            ViewBag.Mesaj = "Kullanıcı adı veya şifre hatalı!";
            return View();




        } // B

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("AnaSayfa");
        } // B

        public string CreatePassword(int size)
        {
            char[] cr = "0123456789ABCDEFGHIJKLMNOPQRSTUCWXYZ".ToCharArray();
            string result = string.Empty;
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                result += cr[r.Next(0, cr.Length - 1)].ToString();
            }

            return result;
        }

        public bool SinifKatilimDenetim(string kod)
        {
            List<Sinif> sinif = ctx.Sinif.ToList();
            foreach (Sinif s in sinif)
            {
                if(s.sinif_kodu == kod)
                {
                    List<Yonetici> yonetici = ctx.Yonetici.ToList();
                    foreach (Yonetici y in yonetici)
                    {
                        if(y.yonetici_id==s.yonetici_id)
                        {
                            List<Kullanicilar> kullanicilar = ctx.Kullanicilar.ToList();
                            foreach (Kullanicilar k in kullanicilar)
                            {
                                if(y.kullanici_id==k.kullanici_id && k.kullanici_adi == User.Identity.Name)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }




    }

}

