using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyModel_DBFirst.Models;

public partial class dbStudentsContext : DbContext
{
    public dbStudentsContext(DbContextOptions<dbStudentsContext> options)
        : base(options)
    {
    }

    //6.1.3 將步驟1.2.4在dbStudentsContext中所寫的空建構子註解掉(也可留著只是用不到)
    //public dbStudentsContext()
    //{
    //}

    // 1.2.4 在dbStudentsContext.cs裡撰寫連線到資料庫的程式
    //6.1.2 將步驟1.2.3在dbStudentsContext中所寫的連線字串註解掉
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=IMCSD-13; Database=dbStudents;TrustServerCertificate=True;User ID = karl; Password=pid/900af");

    public virtual DbSet<tStudent> tStudent { get; set; }
    public virtual DbSet<Department> Department { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tStudent>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PK__tStudent__08E5BA95658F307B");

            entity.Property(e => e.fStuId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.fEmail).HasMaxLength(40);
            entity.Property(e => e.fName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
