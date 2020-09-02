namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sinif")]
    public partial class Sinif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sinif()
        {
            Duyuru = new HashSet<Duyuru>();
            Kontrol_odev = new HashSet<Kontrol_odev>();
            Kontrol_sinif = new HashSet<Kontrol_sinif>();
        }

        [Key]
        public int sinif_id { get; set; }

        [StringLength(30)]
        public string sinif_adi { get; set; }

        [StringLength(8)]
        public string sinif_kodu { get; set; }

        public int? yonetici_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Duyuru> Duyuru { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontrol_odev> Kontrol_odev { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontrol_sinif> Kontrol_sinif { get; set; }

        public virtual Yonetici Yonetici { get; set; }
    }
}
