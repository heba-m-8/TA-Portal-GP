using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.IServices;

namespace TAManagment.Infrastructure.Services;
public class TaskService: ITaskService
{
    private readonly IApplicationDbContext _context;

    public TaskService(IApplicationDbContext context)
    {
        _context = context;
    }
}
