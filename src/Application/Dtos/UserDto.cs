using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UniversityId { get; set; }
    public string DepartmentName { get; set; }
    public string RoleName { get; set; }
    public int? GPA { get; set; }
}
