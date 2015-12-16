using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Models
{
    [Table("AppLog")]
    public partial class AppLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime logDate { get; set; }
        public string msg { get; set; }
    }
}
