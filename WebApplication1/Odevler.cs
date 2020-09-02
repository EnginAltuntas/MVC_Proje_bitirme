namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Odevler")]
    public partial class Odevler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odevler()
        {
            Kontrol_odev = new HashSet<Kontrol_odev>();
        }

        [Key]
        public int odev_id { get; set; }

        [StringLength(200)]
        public string odev { get; set; }

        [StringLength(150)]
        public string odev_tarih { get; set; }

        [StringLength(130)]
        public string odev_saat { get; set; }

        public int? ogrenci_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontrol_odev> Kontrol_odev { get; set; }

        public virtual Ogrenci Ogrenci { get; set; }
    }
}
