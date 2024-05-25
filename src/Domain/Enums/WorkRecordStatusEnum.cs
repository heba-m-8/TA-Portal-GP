using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAManagment.Domain.Enums;
public enum WorkRecordStatusEnum
{   
     NotSubmitted=1,
     IsSubmitted,
     IsApprovedByInstructor,
     IsApprovedByHod,
     IsApprovedByDean,
     IsApprovedByDeanOfGraduates,
     IsApprovedByFinance,
     IsRejectedByInstructor,
     IsRejectedByHod,
     IsRejectedByDean,
     IsRejectedByDeanOfGraduates,
     IsRejectedByFinance,
     IsReSubmitted
}
