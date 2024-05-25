using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class ChangePasswordDto
{
    public string NewPassword {  get; set; }
    public int userId { get; set; }

}
