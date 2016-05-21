
namespace ManageMeDomainEntity
{

  using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockUser")]
public partial class StockUser
{
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; }

    [StringLength(50)]
    public string ticker { get; set; }

    [Column(TypeName = "date")]
    public DateTime? rdate { get; set; }
}
}
