using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Application.Dtos;
public class SectionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int CourseId { get; set; }
    public int CourseRef { get; set; }
    public int? InstructorId { get; set; }
    public string InstructorName { get; set; }
    public string TA { get; set; }
    public int? TAId { get; set; }

}
