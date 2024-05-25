using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Domain.Entities;
public class Section
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }

    public int? InstructorId { get; set; }
    public virtual Users Instructor { get; set; }

    public int? TAId { get; set; }
    public virtual Users TA { get; set; }

    public virtual ICollection<SectionDays> SectionDays { get; set; } = new List<SectionDays>();
    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
}
