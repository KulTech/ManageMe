namespace ManageMeMobileService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DocumentsViewModel
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
       
        public string fileContent { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public int typeid { get; set; }
        public int VendorId { get; set; }
    }
}
