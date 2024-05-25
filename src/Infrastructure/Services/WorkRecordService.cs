using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Enums;

namespace TAManagment.Infrastructure.Services;
public class WorkRecordService: IWorkRecordService
{
    private readonly IApplicationDbContext _context;

    public WorkRecordService(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<WorkRecordDetailsDto> GetWorkRecordById(int workRecordId, int userId)
    {
        var workRecord = await _context.WorkRecord
            .Where(x => x.Id == workRecordId)
            .Select(workRecordDto => new WorkRecordDetailsDto
            {
                Id = workRecordDto.Id,
                StartDate = workRecordDto.StartDate,
                EndDate = workRecordDto.EndDate,
                IsSubmitted = workRecordDto.IsSubmitted,
                IsApprovedByHod = workRecordDto.IsApprovedByHod,
                IsApprovedByDean = workRecordDto.IsApprovedByDean,
                IsApprovedByDeanOfGraduates = workRecordDto.IsApprovedByDeanOfGraduates,
                IsApprovedByFinance = workRecordDto.IsApprovedByFinance,
                IsApprovedByInstructor = workRecordDto.IsApprovedByInstructor,
                Status = workRecordDto.Status,
                InstructorNote = workRecordDto.InstructorNote,
                HODnote = workRecordDto.HODnote,
                DeanNote = workRecordDto.DeanNote,
                DeanOfGraduateStudiesNote = workRecordDto.DeanOfGraduateStudiesNote,
                FinanceNote = workRecordDto.FinanceNote,
                IsApproved = workRecordDto.WorkRecordInstructorApprover
                                            .Where(approver => approver.InstructorId == userId)
                                            .Select(approver => approver.IsApproved)
                                            .FirstOrDefault(),
            }).FirstOrDefaultAsync();

        return workRecord ?? new WorkRecordDetailsDto();
    }




}
