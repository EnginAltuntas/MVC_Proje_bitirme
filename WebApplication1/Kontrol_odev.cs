namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Kontrol_odev
    {
        [Key]
        public int ko_id { get; set; }

        public int? sinif_id { get; set; }

        public int? odev_id { get; set; }

        public int? duyuru_id { get; set; }

        public virtual Duyuru Duyuru { get; set; }

        public virtual Odevler Odevler { get; set; }

        public virtual Sinif Sinif { get; set; }
    }
}
