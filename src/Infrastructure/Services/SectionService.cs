using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.IServices;

namespace TAManagment.Infrastructure.Services;
public class SectionService: ISectionService
{
    private readonly IApplicationDbContext _context;

    public SectionService(IApplicationDbContext context)
    {
        _context = context;
    }
}
