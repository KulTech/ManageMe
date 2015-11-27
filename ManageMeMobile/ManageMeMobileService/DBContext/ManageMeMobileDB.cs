namespace ManageMeMobileService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ManageMeMobileDB : DbContext
    {
        public ManageMeMobileDB()
            : base("name=ManageMeMobileDB")
        {
        }

        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<AppLog> AppLog { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.start_ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.end_ip_address)
                .IsUnicode(false);
            modelBuilder.Entity<AppLog>()
               .Property(e => e.msg)
               .IsUnicode(false);
        }
    }
}
