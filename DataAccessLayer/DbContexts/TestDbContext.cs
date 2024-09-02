using System;
using System.Collections.Generic;
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

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventLog__3214EC070A7D5605");

            entity.ToTable("EventLog");

            entity.HasIndex(e => e.EventDate, "IX_EventLog_EventDate");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.EventDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07BAB68FCC");

            entity.ToTable("Product", tb =>
                {
                    tb.HasTrigger("Tr_Delete_Product");
                    tb.HasTrigger("Tr_Insert_Product");
                    tb.HasTrigger("Tr_Update_Product");
                });

            entity.HasIndex(e => e.Name, "IX_Product_Name");

            entity.HasIndex(e => e.Name, "UQ__Product__737584F64F53F994").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<ProductVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductV__3214EC071874FC5E");

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
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVersions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVersion_Product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07546209C9");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534719A97E1").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456EDC853FF").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
