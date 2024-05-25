using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Domain.Entities;
public class Tasks
{
    public Tasks()
    {
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public float TaskHours { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public bool IsCompleted { get; set; }

    public int AssignedTAId { get; set; }
    public virtual Users AssignedTA { get; set; }
    public DateTime DateCompleted { get; set; }
    public virtual WorkRecord WorkRecord { get; set; }

    public int? WorkRecordId { get; set; }
}
