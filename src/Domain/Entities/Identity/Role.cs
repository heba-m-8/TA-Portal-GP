namespace TAManagment.Domain.Entities.Identity;
public partial class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<Users> Users { get; set; }
}
