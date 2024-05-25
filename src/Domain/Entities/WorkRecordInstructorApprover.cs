using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Domain.Entities;
public class WorkRecordInstructorApprover
{
    public int Id { get; set; }
    public int? WorkRecordId { get; set; }
    public virtual WorkRecord WorkRecord { get; set; }

    public int? InstructorId { get; set; }
    public virtual Users Instructor { get; set; }

    public bool? IsApproved { get; set; } = null;

}
