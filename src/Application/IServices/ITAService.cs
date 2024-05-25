using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface ITAService
{
    Task<List<SectionDto>> GetTASections(int userId);
    Task<List<TaskDto>> GetTATasks(int userId, bool status = false);
    Task<TaskDto> UpdateTask(UpdateTaskDto updateTaskDto);
    Task<List<WorkRecordDto>> GetWorkRecord (int userId, bool IsSubmitted = false);
    Task SubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto);
    public Task<TaskDto> UpdateTaskHours(UpdateTaskDto updateTaskDto);
    public  Task ReSubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto);


}
