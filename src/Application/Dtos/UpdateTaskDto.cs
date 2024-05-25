using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class UpdateTaskDto
{
    public int Id { get; set; }
    public bool? Status { get; set; }
    public float TotalHours { get; set; }
    public float OldTotalHours { get; set; }
}
