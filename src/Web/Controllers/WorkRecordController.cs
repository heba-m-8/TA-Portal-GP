using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkRecordController : ControllerBase
{
    private readonly IWorkRecordService _workRecordService;

    public WorkRecordController(IWorkRecordService workRecordService)
    {
        _workRecordService = workRecordService;
    }



    [HttpGet]
    [Route("GetWorkRecordById")]
    [OpenApiOperation("GetWorkRecordById")]
    public async Task<WorkRecordDetailsDto> GetWorkRecordById(int workRecordId, int userId)
    {
        var workRecord = await _workRecordService.GetWorkRecordById(workRecordId,userId);
        return workRecord;
    }
}
