using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RegistryWebApplication.Models
{
    public partial class DBRegistryContext : DbContext
    {
        public DBRegistryContext()
        {
        }

        public DBRegistryContext(DbContextOptions<DBRegistryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Commission> Commissions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeachersCommission> TeachersCommissions { get; set; }
        public virtual DbSet<Work> Works { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WORKSTATION; Database=DBRegistry; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Commission>(entity =>
            {
                entity.Property(e => e.HeadFathersName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HeadFirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HeadLastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FathersName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FathersName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TeachersCommission>(entity =>
            {
                entity.Property(e => e.DefenseDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Commission)
                    .WithMany(p => p.TeachersCommissions)
                    .HasForeignKey(d => d.CommissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeachersCommissions_Commissions");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeachersCommissions)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeachersCommissions_Teachers");
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Classrooms");

                entity.HasOne(d => d.Commission)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.CommissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Commissions");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_Teachers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
