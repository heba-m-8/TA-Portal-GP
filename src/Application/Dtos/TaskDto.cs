using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Application.Dtos;
public class TaskDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SectionId { get; set; }
    public int AssignedTAId { get; set; }
    public bool Status { get; set; }
    public int CourseRef { get; set; }
    public string Assigner {  get; set; }
    public float? TotalHours { get; set; }
    public string SectionName { get; set; }
    public string InsructorName { get; set; }
}
