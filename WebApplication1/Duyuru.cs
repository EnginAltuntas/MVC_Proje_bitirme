namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Duyuru")]
    public partial class Duyuru
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Duyuru()
        {
            Kontrol_odev = new HashSet<Kontrol_odev>();
        }

        [Key]
        public int duyuru_id { get; set; }

        [StringLength(100)]
        public string baslik { get; set; }

        [StringLength(500)]
        public string bildiri { get; set; }

        [StringLength(300)]
        public string dosya { get; set; }

        public int? sinif_id { get; set; }

        public virtual Sinif Sinif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontrol_odev> Kontrol_odev { get; set; }
    }
}
