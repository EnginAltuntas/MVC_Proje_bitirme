using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace proje_layout_deneme.Controllers
{
    [Authorize]

    public class OyukleController : Controller
    {
        Model1 ctx = new Model1();
        // GET: Oyukle
        [AllowAnonymous] //geçici
        public ActionResult odev_yukle()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]  // geçici
        public ActionResult dosyayukle(HttpPostedFileBase file)  //Eski kullanılmayacak Action
        {
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/aaa"), file.FileName);
                file.SaveAs(path);

                TempData["sonuc"] = file.FileName + " isimli dosya yüklendi." + "Dosyayı " + AppDomain.CurrentDomain.BaseDirectory +
                 "aaa\\" + " bu yolu takip ederek bulabilirsiniz";

                Odevler o = new Odevler();
                o.odev = "~/aaa/" + file.FileName;

                DateTime tarih = DateTime.Now;
                o.odev_tarih = tarih.ToString("dd/MM/yyyy");
                o.odev_saat = tarih.ToString("HH:mm:ss");

                ctx.Odevler.Add(o);
                ctx.SaveChanges();

            }

            return RedirectToAction("odev_yukle");

        }
    }
}