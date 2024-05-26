using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IPhdTAService
{
    Task CreateTask(PhdTaskDto phdTaskDto);
    Task<List<SectionDto>> GetPhdTASections(int userId);
    //Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId);
    Task SubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto);
    Task ReSubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto);


}
