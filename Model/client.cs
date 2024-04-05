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
        [Key]
        [StringLength(25)]
        public string LOGINCLI { get; set; }

        [StringLength(16)]
        public string MDPCLI { get; set; }

        [StringLength(25)]
        public string NOMCLI { get; set; }

        [StringLength(25)]
        public string PRENOMCLI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATENAISCLI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATECREATIONCLI { get; set; }
    }
}
