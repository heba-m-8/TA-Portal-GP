using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhdTAController : ControllerBase
{
    private readonly ITAService _TAService;
    private readonly IPhdTAService _phdTAService;
    public PhdTAController(IPhdTAService phdTAService, ITAService TAService)
    {
       _phdTAService = phdTAService;
       _TAService = TAService;
    }


    [HttpPost]
    [Route("CreateTask")]
    [OpenApiOperation("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] PhdTaskDto phdTaskDto)
    {
        await _phdTAService.CreateTask(phdTaskDto);
        return Ok("Task created successfully.");
    }


    [HttpGet]
    [Route("GetPhdTASections")]
    [OpenApiOperation("GetPhdTASections")]
    public async Task<List<SectionDto>> GetPhdTASections(int userId)
    {
        var sections = await _phdTAService.GetPhdTASections(userId);
        return sections;
    }


    [HttpGet]
    [Route("GetWorkRecord")]
    [OpenApiOperation("GetWorkRecord")]
    public async Task<List<WorkRecordDto>> GetWorkRecord([FromQuery] int userId)
    {
        var workRecord = await _TAService.GetWorkRecord(userId);
        return workRecord;
    }

    [HttpGet]
    [Route("GetRejectedWorkRecords")]
    [OpenApiOperation("GetRejectedWorkRecords")]
    public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    {
        var workRecords = await _phdTAService.GetRejectedWorkRecords(userId);
        return workRecords;
    }

    [HttpPost]
    [Route("SubmitWorkRecord")]
    [OpenApiOperation("SubmitWorkRecord")]
    public async Task<IActionResult> SubmitWorkRecord([FromBody] UpdateWorkRecordDto updateWorkRecordDto)
    {
        await _phdTAService.SubmitWorkRecord(updateWorkRecordDto);
        return Ok("Work record updated successfully.");
    }


    [HttpPost]
    [Route("ReSubmitWorkRecord")]
    [OpenApiOperation("ReSubmitWorkRecord")]
    public async Task<IActionResult> ReSubmitWorkRecord([FromBody] UpdateWorkRecordDto updateWorkRecordDto)
    {
        await _phdTAService.ReSubmitWorkRecord(updateWorkRecordDto);
        return Ok("Work record updated successfully.");
    }
}
