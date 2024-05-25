using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IInstructorService
{
    Task<List<SectionDto>> GetInstructorSections(int userId);
    Task<TADto> GetTAsDetails(int sectionId);
    Task<List<TaskDto>> GetSectionTasks(int sectionId);
    Task AssignTask(TaskDto task);
    Task<List<WorkRecordDto>> GetWorkRecords(int userId);
    Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId);
    Task ApproveWorkRecord(int userId, int workRecordId);
    Task RejectWorkRecord(int userId, int workRecordId, string note);
}

