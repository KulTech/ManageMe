namespace ManageMeDomainEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ManageMeModel : DbContext
    {
        public ManageMeModel()
            : base("name=ManageMeModel")
        {
        }

        public virtual DbSet<AppLog> AppLog { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<SubTypes> SubTypes { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<StockUser> StockUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLog>()
                .Property(e => e.msg)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            //modelBuilder.Entity<Properties>()
            //    .HasMany(e => e.Documents)
            //    .WithRequired(e => e.Property)
            //    .HasForeignKey(e => e.PropertyId)
            //     .WillCascadeOnDelete(false); 

            modelBuilder.Entity<ExpenseType>()
               .Property(e => e.TypeName)
               .IsUnicode(false);
        }
    }
}
