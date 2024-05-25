using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IHODService
{
    Task<List<SectionDto>> GetAllSections(int userId);
    Task<List<TADto>> GetTAsNamesIds(int userId, int sectionId);
    Task<List<TADto>> GetTAsDetails(int userId);
    Task<TADto> ManageTAs(ManageTADto manageTADto);
    Task<List<WorkRecordDto>> GetWorkRecords(int userId);
    Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId);
    Task ApproveWorkRecord(int workRecordId);
    Task RejectWorkRecord(int workRecordId, string note);
}
