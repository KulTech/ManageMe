namespace testConsole
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Documents
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public int PropertyId { get; set; }

        public byte[] fileContent { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public int typeid { get; set; }

        public DateTime updateDate { get; set; }

        public int? VendorId { get; set; }

        public virtual Properties Properties { get; set; }

        public virtual SubTypes SubTypes { get; set; }

        public virtual Vendors Vendors { get; set; }
    }
}
