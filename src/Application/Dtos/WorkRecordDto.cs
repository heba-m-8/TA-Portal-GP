using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Domain.Entities;
using TAManagment.Domain.Entities.Identity;
using TAManagment.Domain.Enums;

namespace TAManagment.Application.Dtos;
public class WorkRecordDto
{
    public int Id { get; set; }
    public float? TotalHours { get; set; }
    public TADto AssignedTA { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime WorkRecordDate { get; set; }
    public List<TaskDto> Tasks { get; set; }

}


public class WorkRecordDetailsDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsSubmitted { get; set; } = false;
    public bool? IsApprovedByInstructor { get; set; } = null;
    public bool? IsApprovedByHod { get; set; } = null;
    public bool? IsApprovedByDean { get; set; } = null;
    public bool? IsApprovedByDeanOfGraduates { get; set; } = null;
    public bool? IsApprovedByFinance { get; set; } = null;
    public WorkRecordStatusEnum Status { get; set; } = WorkRecordStatusEnum.NotSubmitted;

    public bool? IsApproved { get; set; } = null;

    public string InstructorNote { get; set; }
    public string HODnote { get; set; }
    public string DeanNote { get; set; }
    public string DeanOfGraduateStudiesNote { get; set; }
    public string FinanceNote { get; set; }


}
