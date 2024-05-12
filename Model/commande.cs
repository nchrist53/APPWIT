namespace AWIT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("awitbdd.commande")]
    public partial class commande
    {
        [Key]
        public long REFCOM { get; set; }

        [Required]
        [StringLength(25)]
        public string LOGINCLI { get; set; }

        public long IDABO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATECRE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATEDEBUT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATEFIN { get; set; }

        public virtual abonnement abonnement { get; set; }

        public virtual client client { get; set; }
    }
}
