using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController : ControllerBase
{
    private readonly IInstructorService _InstructorService;
    public InstructorController(IInstructorService instructorService)
    {
        _InstructorService = instructorService;
    }


    [HttpGet]
    [Route("GetInstructorSections")]
    [OpenApiOperation("GetInstructorSections")]
    public async Task<List<SectionDto>> GetInstructorSections([FromQuery] int userId)
    {
        var sections = await _InstructorService.GetInstructorSections(userId);
        return sections;
    }


    [HttpGet]
    [Route("GetTAsDetails")]
    [OpenApiOperation("GetTAsDetails")]
    public async Task<TADto> GetTAsDetails([FromQuery] int sectionId)
    {
        var TA = await _InstructorService.GetTAsDetails(sectionId);
        return TA;
    }


    [HttpGet]
    [Route("GetSectionTasks")]
    [OpenApiOperation("GetSectionTasks")]
    public async Task<List<TaskDto>> GetSectionTasks([FromQuery] int sectionId)
    {
        var tasks = await _InstructorService.GetSectionTasks(sectionId);
        return tasks;
    }


    [HttpPost]
    [Route("AssignTask")]
    [OpenApiOperation("AssignTask")]
    public async Task<IActionResult> AssignTask([FromBody] TaskDto taskDto)
    {
        await _InstructorService.AssignTask(taskDto);
        return Ok("Task assigned successfully.");
    }


    [HttpGet]
    [Route("GetWorkRecord")]
    [OpenApiOperation("GetWorkRecord")]
    public async Task<List<WorkRecordDto>> GetWorkRecords(int userId)
    {
        var workRecord = await _InstructorService.GetWorkRecords(userId);
        return workRecord;
    }


    [HttpGet]
    [Route("GetRejectedWorkRecords")]
    [OpenApiOperation("GetRejectedWorkRecords")]
    public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    {
        var workRecords = await _InstructorService.GetRejectedWorkRecords(userId);
        return workRecords;
    }


    [HttpPost]
    [Route("ApproveWorkRecord")]
    [OpenApiOperation("ApproveWorkRecord")]
    public async Task<IActionResult> ApproveWorkRecord([FromQuery] int userId, int workRecordId)
    {
        await _InstructorService.ApproveWorkRecord(userId, workRecordId);
        return Ok("Work record approved successfully.");
    }


    [HttpPost]
    [Route("RejectWorkRecord")]
    [OpenApiOperation("RejectWorkRecord")]
    public async Task<IActionResult> RejectWorkRecord([FromQuery] int userId, int workRecordId, string note)
    {
        await _InstructorService.RejectWorkRecord(userId, workRecordId, note);
        return Ok("Work record rejected successfully.");
    }


}


