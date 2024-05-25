using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Entities.Identity;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HODController : ControllerBase
{
    private readonly IHODService _HODService;
    public HODController(IHODService HODService)
    {
        _HODService = HODService;   
    }

    [HttpGet]
    [Route("GetSections")]
    [OpenApiOperation("GetSections")]
    public async Task<List<SectionDto>> GetAllSections([FromQuery] int userId)
    {
        var sections = await _HODService.GetAllSections(userId);
        return sections;
    }

    [HttpGet]
    [Route("GetTAsNamesIds")]
    [OpenApiOperation("GetTAsNamesIds")]
    public async Task<List<TADto>> GetTAsNamesIds([FromQuery] int userId, [FromQuery] int sectionId)
    {
        var TAs = await _HODService.GetTAsNamesIds(userId, sectionId);
        return TAs;
    }


    [HttpGet]
    [Route("GetTAsDetails")]
    [OpenApiOperation("GetTAsDetails")]
    public async Task<List<TADto>> GetTAsDetails([FromQuery] int userId)
    {
        var TAs = await _HODService.GetTAsDetails(userId);
        return TAs;
    }


    [HttpPost]
    [Route("ManageTAs")]
    [OpenApiOperation("ManageTAs")]
    public async Task<TADto> ManageTAs([FromBody] ManageTADto manageTADto)
    {
        var TA = await _HODService.ManageTAs(manageTADto);
        return TA;
    }


    [HttpGet]
    [Route("GetWorkRecords")]
    [OpenApiOperation("GetWorkRecords")]
    public async Task<List<WorkRecordDto>> GetWorkRecords([FromQuery] int userId)
    {
        var workRecords = await _HODService.GetWorkRecords(userId);
        return workRecords;
    }


    [HttpGet]
    [Route("GetRejectedWorkRecords")]
    [OpenApiOperation("GetRejectedWorkRecords")]
    public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    {
        var workRecords = await _HODService.GetRejectedWorkRecords(userId);
        return workRecords;
    }


    [HttpPost]
    [Route("ApproveWorkRecord")]
    [OpenApiOperation("ApproveWorkRecord")]
    public async Task ApproveWorkRecord([FromQuery] int workRecordId)
    {
        await _HODService.ApproveWorkRecord(workRecordId);
    }


    [HttpPost]
    [Route("RejectWorkRecord")]
    [OpenApiOperation("RejectWorkRecord")]
    public async Task RejectWorkRecord(int workRecordId, string note)
    {
        await _HODService.RejectWorkRecord(workRecordId, note);
    }

}
