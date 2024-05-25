using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinanceController
{
    private readonly IFinanceService _financeService;
    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService; 
    }


    [HttpGet]
    [Route("GetWorkRecords")]
    [OpenApiOperation("GetWorkRecords")]
    public async Task<List<WorkRecordDto>> GetWorkRecords([FromQuery] int userId, [FromQuery] bool? IsApprovedByFinance = null)
    {
        var workRecords = await _financeService.GetWorkRecords(userId, IsApprovedByFinance);
        return workRecords;
    }


    [HttpPost]
    [Route("ApproveWorkRecord")]
    [OpenApiOperation("ApproveWorkRecord")]
    public async Task ApproveWorkRecord([FromQuery] int workRecordId)
    {
        await _financeService.ApproveWorkRecord(workRecordId);
    }


    [HttpPost]
    [Route("RejectWorkRecord")]
    [OpenApiOperation("RejectWorkRecord")]
    public async Task RejectWorkRecord(int workRecordId, string note)
    {
        await _financeService.RejectWorkRecord(workRecordId, note);
    }

}
