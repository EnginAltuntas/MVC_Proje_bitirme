using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repository;
using System.Threading.Tasks;

namespace WebApplication1.Contollers
{
    [Authorize]
    public class BlobController : Controller
    {
        Model1 ctx = new Model1();
        private readonly IBlobStorageRepository repo;

        public ActionResult aaaaaaaaaaaaaaaa()
        {
            return View();
        }

        public ActionResult aaadeneme()
        {
            return View();
        }

        public BlobController(IBlobStorageRepository _repo)
        {
            this.repo = _repo;
        }

        // GET: Blob
        public ActionResult Index()
        {
            var blobVM = repo.GetBlobs();
            return View(blobVM);
        }
        
        public JsonResult RemoveBlob(string file,string extension,int id)
        {

            Kontrol_odev ko = ctx.Kontrol_odev.FirstOrDefault(x => x.odev_id == id);
            ctx.Kontrol_odev.Remove(ko);
            ctx.SaveChanges();

            Odevler odevler = ctx.Odevler.FirstOrDefault(x => x.odev_id == id);
            ctx.Odevler.Remove(odevler);
            ctx.SaveChanges();

            bool isDeleted = repo.DeleteBlob(file,extension);

            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DownloadBlob(string file,string extension)
        {
            bool isDownloaded = await repo.DownloadBlobAsync(file,extension);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UploadBlob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadBlob(HttpPostedFileBase uploadFileName,int nereye,int kime,int duyuru,int sid)
        {
            bool isUploaded = repo.UploadBlob(uploadFileName,nereye,kime,duyuru);
            if(isUploaded==true)
            {

                return RedirectToAction("goster", "Siniflar", new {  id = sid });
            }
            else
            {

            }
            return View();

        }


    }
}