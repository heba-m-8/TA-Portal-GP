using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IFinanceService
{
    Task<List<WorkRecordDto>> GetWorkRecords(int userId, bool? IsApprovedByFinance = null);
    Task ApproveWorkRecord(int workRecordId);
    Task RejectWorkRecord(int workRecordId, string note);
}
