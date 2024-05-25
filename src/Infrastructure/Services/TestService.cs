using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;

namespace TAManagment.Infrastructure.Services;
public class TestService : ITestService
{
    private readonly IApplicationDbContext _context;

    public TestService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TestDto> AddTestDto(TestDto testDto)
    {
   
        await _context.SaveChangesAsync(default);
        return new();
    }
}
