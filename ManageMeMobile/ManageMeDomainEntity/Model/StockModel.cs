using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMeDomainEntity.StockModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StockModel : DbContext
    {
        public StockModel()
            : base("name=StockModel")
        {
        }

        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<AppLog> AppLog { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { }
        }
}
