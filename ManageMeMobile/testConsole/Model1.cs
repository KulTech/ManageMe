namespace testConsole
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model16")
        {
        }

        public virtual DbSet<AppLog> AppLog { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<ExpenseType> ExpenseType { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<StockUser> StockUser { get; set; }
        public virtual DbSet<SubTypes> SubTypes { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }

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

            modelBuilder.Entity<Documents>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ExpenseType>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseType>()
                .HasMany(e => e.SubTypes)
                .WithRequired(e => e.ExpenseType)
                .HasForeignKey(e => e.ETypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Properties>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Properties)
                .HasForeignKey(e => e.PropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubTypes>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.SubTypes)
                .HasForeignKey(e => e.typeid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vendors>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.Vendors)
                .HasForeignKey(e => e.VendorId);
        }
    }
}
