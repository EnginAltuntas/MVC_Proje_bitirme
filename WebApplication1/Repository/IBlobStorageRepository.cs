using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Repository
{
    public interface IBlobStorageRepository
    {
        IEnumerable<BlobViewModel> GetBlobs();
        bool DeleteBlob(string file, string fileExtension);
        bool UploadBlob(HttpPostedFileBase blobFile,int nereye,int kime,int duyuru);

        Task<bool> DownloadBlobAsync(string file, string fileExtension);


    }
}