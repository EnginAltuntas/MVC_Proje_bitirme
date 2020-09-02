using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace WebApplication1.Repository
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        public static string _isim;
        private StorageCredentials _storageCredentialsx;
        private CloudStorageAccount _cloudStorageAccountx;
        private CloudBlobClient _cloudBlobClientx;
        private CloudBlobContainer _cloudBlobContainerx;
        private string containerNamex = "proje";
        private string downloadPath = @"C:\AZ\";
        public BlobStorageRepository()
        {
            string accountNamex = "dosyalar";
            string keyx = "waCUNucc3kA8e0S/5acrtY4/aZgTPiWOkyEAqBJnrVNJkJpHrIZZHztEt37pyKFwMwbD1EfsXyppM/POq31gsQ==";

            _storageCredentialsx = new StorageCredentials(accountNamex, keyx);
            _cloudStorageAccountx = new CloudStorageAccount(_storageCredentialsx,true);
            _cloudBlobClientx = _cloudStorageAccountx.CreateCloudBlobClient();
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);

        }
        public bool DeleteBlob(string file, string fileExtension)
        {
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);

            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(file + "." + fileExtension); // blob türü
            bool deleted = blockBlob.DeleteIfExists();



            return deleted;
          //  throw new NotImplementedException();
        }

        public async Task<bool> DownloadBlobAsync(string file, string fileExtension)
        {
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(file + "." + fileExtension);

            using (var fileStream = System.IO.File.OpenWrite(downloadPath + file + "." + fileExtension))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
                return true;
            }
        }

        public IEnumerable<BlobViewModel> GetBlobs()
        {
            var context = _cloudBlobContainerx.ListBlobs().ToList();
            IEnumerable<BlobViewModel> VM = context.Select(x => new BlobViewModel
            {
                BlobContainerName = x.Container.Name,
                StorageUri = x.StorageUri.PrimaryUri.ToString(),
                PrimaryUri = x.StorageUri.PrimaryUri.ToString(),
                ActualFileName = x.Uri.AbsoluteUri.Substring(x.Uri.AbsoluteUri.LastIndexOf("/")+1),
                fileExtension = System.IO.Path.GetExtension(x.Uri.AbsoluteUri.Substring(x.Uri.AbsoluteUri.LastIndexOf("/")+1))

            }).ToList();
            return VM;
        }
        Model1 ctx = new Model1();
        public bool UploadBlob(HttpPostedFileBase blobFile,int nereye,int kime,int duyuru)
        {
           if(blobFile == null)
            {
                return false;
            }

            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(blobFile.FileName);

            using (var fileStream = (blobFile.InputStream))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            
            if(nereye == 1)
            {
                _isim = _cloudBlobContainerx.Uri.ToString() + "/" + blobFile.FileName; // indirme linki
            }
            else if (nereye == 2)
            {
                /*
                List<Odevler> odevler = ctx.Odevler.ToList();
                foreach (Odevler od in odevler)
                {
                    if(od.ogrenci_id==kime)
                    {
                        List<Kontrol_odev> kontrol_odev = ctx.Kontrol_odev.ToList();
                        foreach (Kontrol_odev kk in kontrol_odev)
                        {
                            if(kk.duyuru_id==duyuru && od.odev_id==kk.odev_id)
                            {
                                return false;
                            }
                        }
                    }
                }

    */


                // Veri Tabanına ekleme burada yapıldı
                Odevler o = new Odevler();
                o.odev = _cloudBlobContainerx.Uri.ToString() + "/" + blobFile.FileName;
                DateTime tarih = DateTime.Now;
                o.odev_tarih = tarih.ToString("dd/MM/yyyy");
                o.odev_saat = tarih.ToString("HH:mm:ss");
                o.ogrenci_id = kime; // öğrenci_id

                ctx.Odevler.Add(o);
                ctx.SaveChanges();
                Kontrol_odev ko = new Kontrol_odev();
                ko.odev_id = o.odev_id;

                List<Duyuru> Duyuru = ctx.Duyuru.ToList();
                foreach (Duyuru d in Duyuru)
                {
                    if(d.duyuru_id==duyuru) //duyuru_id
                    {
                        ko.duyuru_id = d.duyuru_id;
                        ko.sinif_id=d.Sinif.sinif_id;
                        break;
                    }
                }

                ctx.Kontrol_odev.Add(ko);
                ctx.SaveChanges();


            }
            return true;

        }
    }
}