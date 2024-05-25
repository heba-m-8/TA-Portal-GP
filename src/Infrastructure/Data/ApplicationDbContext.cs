using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Domain.Entities;
using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options) {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Users> User => Set<Users>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<Course> Course => Set<Course>();
    public DbSet<Department> Department => Set<Department>();
    public DbSet<School> School => Set<School>();
    public DbSet<Section> Section => Set<Section>();
    public DbSet<SectionDays> SectionDays => Set<SectionDays>();
    public DbSet<Domain.Entities.Tasks> Tasks => base.Set<Domain.Entities.Tasks>();
    public DbSet<WorkRecord> WorkRecord => Set<WorkRecord>();
    public DbSet<WorkRecordInstructorApprover> WorkRecordInstructorApprover => Set<WorkRecordInstructorApprover>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>()
            .HasMany(u => u.SectionsAsInstructor)
            .WithOne(s => s.Instructor)
            .HasForeignKey(s => s.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Users>()
            .HasMany(u => u.SectionsAsTA)
            .WithOne(s => s.TA)
            .HasForeignKey(s => s.TAId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WorkRecord>()
           .HasMany(u => u.WorkRecordInstructorApprover)
           .WithOne(s => s.WorkRecord)
           .HasForeignKey(s => s.WorkRecordId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
            .HasOne(u => u.WorkRecord)
            .WithMany(s => s.Tasks)
            .HasForeignKey(s => s.WorkRecordId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
