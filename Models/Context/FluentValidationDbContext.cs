using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DomainModels;

namespace Models.Context;

public class FluentValidationDbContext : DbContext
{
    public FluentValidationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>()
            .HasOne(o => o.Student)
            .WithMany(o => o.StudentCourses)
            .HasForeignKey(o => o.StudentId);


        modelBuilder.Entity<StudentCourse>()
            .HasOne(o => o.Course)
            .WithMany(o => o.StudentCourses)
            .HasForeignKey(o => o.CourseId);

        modelBuilder.Entity<Address>()
            .HasOne(o => o.Student)
            .WithMany(o => o.Addresses)
            .HasForeignKey(o => o.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasKey(o => new { o.StudentId, o.CourseId });
    }

    public DbSet<Course>? Course { get; set; }
    public DbSet<Address>? Address { get; set; }
    public DbSet<Student>? Student { get; set; }
    public DbSet<StudentCourse>? StudentCourse { get; set; }
}

