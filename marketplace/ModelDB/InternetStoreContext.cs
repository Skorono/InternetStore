using System;
using System.Collections.Generic;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetStore.ModelBD;

public partial class InternetStoreContext : DbContext
{
    private static InternetStoreContext? _context;

    private InternetStoreContext() { }

    private InternetStoreContext(DbContextOptions<InternetStoreContext> options)
        : base(options)
    {
    }

    public static InternetStoreContext getInstance()
    {
        if (_context == null)
            _context = new InternetStoreContext();
        return _context;
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPersonalInf> UserPersonalInfs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G0VP87F;Database=InternetStore;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Basket");

            entity.Property(e => e.AddDate)
                .HasColumnType("datetime")
                .HasColumnName("add_date");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Basket.FK_PRODUCT_ID");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Basket.FK_USER_ID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category.PK_ID");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order.PK_ORDER_ID");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.DatatimeOfForm)
                .HasColumnType("datetime")
                .HasColumnName("datatime_of_form");
            entity.Property(e => e.DatatimeOfPayment)
                .HasColumnType("datetime")
                .HasColumnName("datatime_of_payment");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Order.FK_USER_ID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product.PK_ID");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.ProductName)
                .IsUnicode(false)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Product.FK_CATEGORY_ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CCD89F97D7");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__783254B1AFA2B73B").IsUnique();

            entity.Property(e => e.RoleId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F29E2098C");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164387ECCC8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__User__role_id__49C3F6B7");
        });

        modelBuilder.Entity<UserPersonalInf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserPersonalInf");

            entity.HasIndex(e => e.PhoneNumber, "UQ__UserPers__A1936A6B20DF7058").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MiddleName)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Surname)
                .IsUnicode(false)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserPersonal__id__74AE54BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
