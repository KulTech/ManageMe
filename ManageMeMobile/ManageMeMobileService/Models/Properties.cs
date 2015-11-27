using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMeMobileService
{
    [Table("Properties")]
    public partial class Properties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
