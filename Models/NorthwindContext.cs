﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API.Models
{
    //* IdentityDbContext contains all the user tables
    public partial class NorthwindContext : IdentityDbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        //public virtual DbSet<Category> Category { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //         // optionsBuilder.UseSqlServer("Server=LENOVO;Database=Northwind;Trusted_Connection=True;");
        //     }
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Category>(entity =>
        //     {
        //         entity.ToTable("Category");

        //         entity.Property(e => e.CategoryId)
        //             .ValueGeneratedNever()
        //             .HasColumnName("CategoryID");

        //         entity.Property(e => e.CategoryName).HasMaxLength(50);

        //         entity.Property(e => e.Description).HasMaxLength(100);
        //     });

        //     OnModelCreatingPartial(modelBuilder);
        // }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
