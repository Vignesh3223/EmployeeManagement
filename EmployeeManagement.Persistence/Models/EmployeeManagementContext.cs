using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Models;

public partial class EmployeeManagementContext : DbContext
{
    public EmployeeManagementContext()
    {
    }

    public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<DesignationMaster> DesignationMasters { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<SalaryMaster> SalaryMasters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07D2D5742D");

            entity.ToTable("DepartmentMaster");

            entity.Property(e => e.Departmentname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Designat__3214EC072D7841DA");

            entity.ToTable("DesignationMaster");

            entity.Property(e => e.DesignationName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.DesignationMasters)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Designati__Depar__4D94879B");
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07380C927B");

            entity.ToTable("EmployeeMaster");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeeMasters)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__EmployeeM__Depar__59063A47");

            entity.HasOne(d => d.Designation).WithMany(p => p.EmployeeMasters)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK__EmployeeM__Desig__59FA5E80");
        });

        modelBuilder.Entity<SalaryMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalaryMa__3214EC07A3C4D2C0");

            entity.ToTable("SalaryMaster");

            entity.HasOne(d => d.Department).WithMany(p => p.SalaryMasters)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__SalaryMas__Depar__5070F446");

            entity.HasOne(d => d.Designation).WithMany(p => p.SalaryMasters)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK__SalaryMas__Desig__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
