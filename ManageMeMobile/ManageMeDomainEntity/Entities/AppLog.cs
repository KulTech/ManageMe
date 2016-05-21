namespace ManageMeDomainEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppLog")]
    public partial class AppLog
    {
        public int Id { get; set; }

        public DateTime LogDate { get; set; }

        [StringLength(2500)]
        public string msg { get; set; }
    }
}
