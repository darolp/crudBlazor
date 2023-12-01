using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.server.Models;

public partial class DbcrudBlazorContext : DbContext
{
    public DbcrudBlazorContext()
    {
    }

    public DbcrudBlazorContext(DbContextOptions<DbcrudBlazorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).HasName("PK__Departme__DF1E6E4BCF501760");

            entity.ToTable("Department");

            entity.Property(e => e.IdDepartment).ValueGeneratedNever();
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__51C8DD7A835D3B2B");

            entity.ToTable("Employee");

            entity.Property(e => e.DateContract).HasColumnType("date");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDepartment)
                .HasConstraintName("FK__Employee__IdDepa__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
