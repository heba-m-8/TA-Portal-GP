using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class TADto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UniversityId { get; set; }
    public int? GPA { get; set; }

    public string School { get; set; }
    public string DepName { get; set; }
    public int? DepartmentId { get; set; }
    public int? RoleId { get; set; }

}
