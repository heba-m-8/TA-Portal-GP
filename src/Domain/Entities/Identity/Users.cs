namespace TAManagment.Domain.Entities.Identity;
public class Users
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UniversityId { get; set; }
    public bool IsActive { get; set; } = false;
    public int? DepartmentId { get; set; }
    public int? RoleId { get; set; }
    public int? GPA { get; set; }
    public virtual Department Department { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    public virtual ICollection<Section> SectionsAsInstructor { get; set; } = new List<Section>();
    public virtual ICollection<Section> SectionsAsTA { get; set; } = new List<Section>();
    public byte[] HashedPassword { get; set; }
    public byte[] Salt { get; set; }

}
