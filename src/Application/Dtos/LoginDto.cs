using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class LoginDto
{
    public int ID { set; get; }
    public string UniversityId { get; set; }
    public string UserPassword { get; set; }

    public string RoleName { set; get; }
}
