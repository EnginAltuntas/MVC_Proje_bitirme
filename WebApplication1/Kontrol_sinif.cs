namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Kontrol_sinif
    {
        [Key]
        public int ks_id { get; set; }

        public int? sinif_id { get; set; }

        public int? ogrenci_id { get; set; }

        public virtual Ogrenci Ogrenci { get; set; }

        public virtual Sinif Sinif { get; set; }
    }
}
