using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Domain.Entities;
public class WorkRecord
{
    public int Id { get; set; }
    public float? TotalHours { get; set; }
    public int AssignedTAId { get; set; }
    public virtual Users AssignedTA { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime WorkRecordDate { get; set; }
    public bool IsSubmitted { get; set; } = false;
    public bool? IsApprovedByInstructor { get; set; } = null;
    public bool? IsApprovedByHod { get; set; } = null;
    public bool? IsApprovedByDean { get; set; } = null;
    public bool? IsApprovedByDeanOfGraduates { get; set; } = null;
    public bool? IsApprovedByFinance { get; set; } = null;
    public WorkRecordStatusEnum Status { get; set; } = WorkRecordStatusEnum.NotSubmitted;
    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    public virtual ICollection<WorkRecordInstructorApprover> WorkRecordInstructorApprover { get; set; } = new List<WorkRecordInstructorApprover>();
    public string InstructorNote { get; set; }
    public string HODnote { get; set; }
    public string DeanNote { get; set; }
    public string DeanOfGraduateStudiesNote { get; set; }
    public string FinanceNote { get; set; }
}
