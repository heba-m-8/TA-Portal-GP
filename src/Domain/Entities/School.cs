namespace TAManagment.Domain.Entities;
public class School
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

}
