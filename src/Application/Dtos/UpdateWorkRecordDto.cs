using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Domain.Enums;

namespace TAManagment.Application.Dtos;
public class UpdateWorkRecordDto
{
    public int Id { get; set; }
    public WorkRecordStatusEnum Status { get; set; } = WorkRecordStatusEnum.NotSubmitted;
    public int AssignedTAId { get; set; }

}
