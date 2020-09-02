namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yonetici")]
    public partial class Yonetici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yonetici()
        {
            Sinif = new HashSet<Sinif>();
        }

        [Key]
        public int yonetici_id { get; set; }

        public int? kullanici_id { get; set; }

        public virtual Kullanicilar Kullanicilar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sinif> Sinif { get; set; }
    }
}
