using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using cw12.Models;

namespace cw12.Data;

public partial class ApbdContext : DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    public virtual DbSet<ComponentType> ComponentTypes { get; set; }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<Pccomponent> Pccomponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.HasIndex(e => e.ComponentManufacturersId, "IX_Components_ComponentManufacturersId");

            entity.HasIndex(e => e.ComponentTypesId, "IX_Components_ComponentTypesId");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(300);

            entity.HasOne(d => d.ComponentManufacturers).WithMany(p => p.Components).HasForeignKey(d => d.ComponentManufacturersId);

            entity.HasOne(d => d.ComponentTypes).WithMany(p => p.Components).HasForeignKey(d => d.ComponentTypesId);
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.Property(e => e.Abbrevation).HasMaxLength(30);
            entity.Property(e => e.FullName).HasMaxLength(300);
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.Property(e => e.Abbrevation).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.ToTable("PCs");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Pccomponent>(entity =>
        {
            entity.HasKey(e => new { e.Pcid, e.ComponentCode });

            entity.ToTable("PCComponents");

            entity.HasIndex(e => e.ComponentCode, "IX_PCComponents_ComponentCode");

            entity.Property(e => e.Pcid).HasColumnName("PCId");
            entity.Property(e => e.ComponentCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ComponentCodeNavigation).WithMany(p => p.Pccomponents).HasForeignKey(d => d.ComponentCode);

            entity.HasOne(d => d.Pc).WithMany(p => p.Pccomponents).HasForeignKey(d => d.Pcid);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
