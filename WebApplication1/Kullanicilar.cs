namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanicilar")]
    public partial class Kullanicilar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanicilar()
        {
            Ogrenci = new HashSet<Ogrenci>();
            Yonetici = new HashSet<Yonetici>();
        }

        [Key]
        public int kullanici_id { get; set; }

        [StringLength(30)]
        public string ad { get; set; }

        [StringLength(30)]
        public string soyad { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(30)]
        public string sehir { get; set; }

        [StringLength(1)]
        public string cinsiyet { get; set; }

        [StringLength(20)]
        public string kullanici_adi { get; set; }

        [StringLength(20)]
        public string sifre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogrenci> Ogrenci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yonetici> Yonetici { get; set; }
    }
}
