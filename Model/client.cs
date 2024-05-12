namespace AWIT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("awitbdd.client")]
    public partial class client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public client()
        {
            commandes = new HashSet<commande>();
        }
        
        [Key]
        [StringLength(maximumLength: 25)]
        public string LOGINCLI { get; set; }

        [StringLength(maximumLength: 16)]
        public string MDPCLI { get; set; }

        [StringLength(maximumLength: 25)]
        public string NOMCLI { get; set; }

        [StringLength(maximumLength: 25)]
        public string PRENOMCLI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATENAISCLI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATECREATIONCLI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commande> commandes { get; set; }
    }
}
