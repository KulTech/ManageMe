using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMeDomainEntity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubTypes")]
    public partial class SubTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubTypes()
        {
            
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SubTypeName { get; set; }
        [Required]
        public int ETypeId { get; set; }

        [ForeignKey("ETypeId")]
        public virtual ExpenseType EType { get; set; }
    }
}
