namespace TAManagment.Domain.Entities;
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual int DepartmentId { get; set; }
    public int CourseRef { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
