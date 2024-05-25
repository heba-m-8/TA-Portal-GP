using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class PhdTaskDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SectionId { get; set; }
    public bool Status { get; set; }
    public int Hours { get; set; }
    public int AssignedTAId { get; set; }
}
