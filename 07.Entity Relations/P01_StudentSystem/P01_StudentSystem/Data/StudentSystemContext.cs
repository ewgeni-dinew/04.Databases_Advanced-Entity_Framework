using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
       public StudentSystemContext()
        {
        
        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionStr);
            }
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity => 
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode();

                entity.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .IsUnicode(false);

                entity.Property(e => e.Birthday)
                .IsRequired(false);

                entity.HasMany(s => s.HomeworkSubmissions)
                     .WithOne(h => h.Student)
                     .HasForeignKey(h => h.StudentId)
                     .HasConstraintName("FK_Students_Homeworks");
            });

            modelBuilder.Entity<Course>(entity=>
            {
                entity.HasKey(e=>e.CourseId);

                entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode();

                entity.Property(e => e.Description)
                .IsUnicode()
                .IsRequired(false);

                entity.HasMany(c => c.Resources)
                      .WithOne(r => r.Course)
                      .HasForeignKey(r => r.CourseId)
                      .HasConstraintName("FK_Courses_Resources");

                entity.HasMany(c => c.HomeworkSubmissions)
                      .WithOne(h => h.Course)
                      .HasForeignKey(h => h.CourseId)
                      .HasConstraintName("FK_Courses_Homeworks");

            });

            modelBuilder.Entity<Homework>(entity=> 
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.Content)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Resource>(entity=> 
            {
                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode();

                entity.Property(e => e.Url)
                .IsUnicode(false);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(a => new { a.StudentId, a.CourseId });

                entity.HasOne(sc => sc.Student)
                      .WithMany(s => s.CourseEnrollments)
                      .HasForeignKey(sc => sc.StudentId);

                entity.HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(sc => sc.CourseId);

            });
        }
    }
}
