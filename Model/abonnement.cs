namespace AWIT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("awitbdd.abonnement")]
    public partial class abonnement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public abonnement()
        {
            commandes = new HashSet<commande>();
            musiques = new HashSet<musique>();
        }

        [Key]
        public long IDABO { get; set; }

        [StringLength(25)]
        public string NOM { get; set; }

        public string DESCRIPTION { get; set; }

        public decimal? PRIXMENSUEL { get; set; }

        [Required]
        public string IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commande> commandes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<musique> musiques { get; set; }
    }
}
