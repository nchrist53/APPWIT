namespace AWIT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("awitbdd.messenger_messages")]
    public partial class messenger_messages
    {
        public long id { get; set; }

        [Required]
        public string body { get; set; }

        [Required]
        public string headers { get; set; }

        [Required]
        [StringLength(190)]
        public string queue_name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime available_at { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? delivered_at { get; set; }
    }
}
