using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EventLog> EventLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVersion> ProductVersions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventLog__3214EC074F3B7DD0");

            entity.ToTable("EventLog");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.EventDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07CE347D22");

            entity.ToTable("Product", tb =>
                {
                    tb.HasTrigger("Tr_Delete_Product");
                    tb.HasTrigger("Tr_Insert_Product");
                    tb.HasTrigger("Tr_Update_Product");
                });

            entity.HasIndex(e => e.Name, "IX_Product_Name");

            entity.HasIndex(e => e.Name, "UQ__Product__737584F6D09D5F6D").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductV__3214EC0736686390");

            entity.ToTable("ProductVersion", tb =>
                {
                    tb.HasTrigger("Tr_Delete_ProductVersion");
                    tb.HasTrigger("Tr_Insert_ProductVersion");
                    tb.HasTrigger("Tr_Update_ProductVersion");
                });

            entity.HasIndex(e => e.CreatingDate, "IX_ProductVersion_CreatingDate");

            entity.HasIndex(e => e.Height, "IX_ProductVersion_Height");

            entity.HasIndex(e => e.Length, "IX_ProductVersion_Length");

            entity.HasIndex(e => e.Name, "IX_ProductVersion_Name");

            entity.HasIndex(e => e.Width, "IX_ProductVersion_Width");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVersions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVersion_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
