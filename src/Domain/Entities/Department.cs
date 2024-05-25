namespace TAManagment.Domain.Entities;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual int SchoolId { get; set; }
    public virtual School School { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

}
