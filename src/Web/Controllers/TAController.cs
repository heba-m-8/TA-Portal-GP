using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;

namespace TAManagment.Web.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TAController : ControllerBase
{
    private readonly ITAService _TAService;
    public TAController(ITAService TAService)
    {
        _TAService = TAService;
    }


    [HttpGet]
    [Route("GetTASections")]
    [OpenApiOperation("GetTASections")]
    public async Task<List<SectionDto>> GetATASections([FromQuery] int userId)
    {
        var sections = await _TAService.GetTASections(userId);
        return sections;
    }


    [HttpGet]
    [Route("GetTATasks")]
    [OpenApiOperation("GetTATasks")]
    public async Task<List<TaskDto>> GetTATasks([FromQuery] int userId, bool status = false)
    {
        var tasks = await _TAService.GetTATasks(userId,status);
        return tasks;
    }


    [HttpPost]
    [Route("UpdateTask")]
    [OpenApiOperation("UpdateTask")]
    public async Task<TaskDto> UpdateTask([FromBody] UpdateTaskDto updateTaskDto)
    {
        var task = await _TAService.UpdateTask(updateTaskDto);
        return task;
    }


    [HttpGet]
    [Route("GetWorkRecord")]
    [OpenApiOperation("GetWorkRecord")]
    public async Task<List<WorkRecordDto>> GetWorkRecord([FromQuery] int userId, [FromQuery] bool IsSubmitted = false)
    {
        var workRecord = await _TAService.GetWorkRecord(userId, IsSubmitted);
        return workRecord;
    }


    [HttpPost]
    [Route("SubmitWorkRecord")]
    [OpenApiOperation("SubmitWorkRecord")]
    public async Task<IActionResult> SubmitWorkRecord([FromBody] UpdateWorkRecordDto updateWorkRecordDto)
    {
        await _TAService.SubmitWorkRecord(updateWorkRecordDto);
        return Ok("Work record updated successfully.");
    }



    [HttpPost]
    [Route("UpdateTaskHours")]
    [OpenApiOperation("UpdateTaskHours")]
    public async Task<TaskDto> UpdateTaskHours([FromBody] UpdateTaskDto updateTaskDto)
    {
        var task = await _TAService.UpdateTaskHours(updateTaskDto);
        return task;
    }



    [HttpPost]
    [Route("ReSubmitWorkRecord")]
    [OpenApiOperation("ReSubmitWorkRecord")]
    public async Task<IActionResult> ReSubmitWorkRecord([FromBody] UpdateWorkRecordDto updateWorkRecordDto)
    {
        await _TAService.ReSubmitWorkRecord(updateWorkRecordDto);
        return Ok("Work record updated successfully.");
    }
}

