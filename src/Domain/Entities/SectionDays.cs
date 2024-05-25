namespace TAManagment.Domain.Entities;
public class SectionDays
{
    public int Id { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public DayOfWeekEnum DayOfWeekEnum { get; set; }
}
