using Microsoft.AspNetCore.Mvc;
using TAManagment.Application.IServices;

namespace TAManagment.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [HttpGet] // Example action method
    public IActionResult GetTasks()
    {
        // Implement your logic here
        return Ok("List of tasks");
    }

}
