namespace AWIT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("awitbdd.musique")]
    public partial class musique
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public musique()
        {
            abonnements = new HashSet<abonnement>();
            auteurs = new HashSet<auteur>();
            groupes = new HashSet<groupe>();
        }

        [Key]
        public long REFMUS { get; set; }

        public long IDALBUM { get; set; }

        [StringLength(50)]
        public string TITRE { get; set; }

        [StringLength(255)]
        public string SON { get; set; }

        public string PAROLE { get; set; }

        public virtual album album { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<abonnement> abonnements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<auteur> auteurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<groupe> groupes { get; set; }
    }
}
