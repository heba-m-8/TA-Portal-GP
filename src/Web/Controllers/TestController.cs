using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;

namespace TAManagment.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpPost]
    public async Task<TestDto> AddTestDto(TestDto testDto) 
    { 
       var result = await _testService.AddTestDto(testDto);
       return result;
    }
}

