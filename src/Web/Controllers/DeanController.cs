using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeanController
{
    private readonly IDeanService _DeanService;
    public DeanController(IDeanService deanService)
    {
        _DeanService = deanService;
    }


    [HttpGet]
    [Route("GetWorkRecords")]
    [OpenApiOperation("GetWorkRecords")]
    public async Task<List<WorkRecordDto>> GetWorkRecords([FromQuery] int userId)
    {
        var workRecords = await _DeanService.GetWorkRecords(userId);
        return workRecords;
    }


    [HttpGet]
    [Route("GetRejectedWorkRecords")]
    [OpenApiOperation("GetRejectedWorkRecords")]
    public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    {
        var workRecords = await _DeanService.GetRejectedWorkRecords(userId);
        return workRecords;
    }


    [HttpPost]
    [Route("ApproveWorkRecord")]
    [OpenApiOperation("ApproveWorkRecord")]
    public async Task ApproveWorkRecord([FromQuery] int workRecordId)
    {
        await _DeanService.ApproveWorkRecord(workRecordId);
    }


    [HttpPost]
    [Route("RejectWorkRecord")]
    [OpenApiOperation("RejectWorkRecord")]
    public async Task RejectWorkRecord(int workRecordId, string note)
    {
        await _DeanService.RejectWorkRecord(workRecordId, note);
    }

}
