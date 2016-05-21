namespace ManageMeDomainEntity
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
       
        public Byte[] fileContent { get; set; }

        public int PropertyId { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public int typeid { get; set; }

        public int VendorId { get; set; }
        [ForeignKey("typeid")]
        public virtual SubTypes SubType { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Properties Property { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendors Vendor { get; set; }
    }
}
