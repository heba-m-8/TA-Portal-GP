using Microsoft.AspNetCore.Mvc;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;

namespace TAManagment.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SectionController : ControllerBase
{
    private readonly ITestService _testService;

    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService,ITestService testService)
    {
        _sectionService = sectionService;
        _testService = testService;
    }

    [HttpPost]
    public async Task<TestDto> AddTestDto(TestDto testDto)
    {
        var result = await _testService.AddTestDto(testDto);
        return result;
    }

}

