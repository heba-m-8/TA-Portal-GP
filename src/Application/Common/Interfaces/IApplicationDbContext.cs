

using TAManagment.Domain.Entities;
using TAManagment.Domain.Entities.Identity;
namespace TAManagment.Application.Common.Interfaces;

 public interface IApplicationDbContext
{
    DbSet<Users> User { get; }
    DbSet<Role> Role { get; }
    DbSet<Course> Course { get; }
    DbSet<Department> Department { get; }
    DbSet<School> School { get; }
    DbSet<Section> Section { get; }
    DbSet<SectionDays> SectionDays { get; }
    DbSet<Domain.Entities.Tasks> Tasks  { get; }
    DbSet<WorkRecord> WorkRecord { get; }
    DbSet<WorkRecordInstructorApprover> WorkRecordInstructorApprover { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
