using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Enums;

namespace TAManagment.Infrastructure.Services;
public class DeanService : IDeanService
{
    private readonly IApplicationDbContext _context;

    public DeanService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkRecordDto>> GetWorkRecords(int userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new List<WorkRecordDto>();
        }

        var workRecordsList = await _context.WorkRecord
            .Where(w => w.AssignedTA.DepartmentId == user.DepartmentId 
                && w.IsApprovedByHod == true
                && w.IsApprovedByDean == null)
            .Select(workRecordDto => new WorkRecordDto
            {
                Id = workRecordDto.Id,
                TotalHours = workRecordDto.TotalHours,
                AssignedTA = new TADto
                {
                    Id = workRecordDto.AssignedTAId,
                    UserName = workRecordDto.AssignedTA.UserName,
                    UserEmail = workRecordDto.AssignedTA.UserEmail,
                    UniversityId = workRecordDto.AssignedTA.UniversityId,
                    GPA = workRecordDto.AssignedTA.GPA,
                    DepName = workRecordDto.AssignedTA.Department.Name,
                    School = workRecordDto.AssignedTA.Department.School.Name
                },
                StartDate = workRecordDto.StartDate,
                EndDate = workRecordDto.EndDate,
                WorkRecordDate = workRecordDto.WorkRecordDate,
                Tasks = workRecordDto.Tasks.Select(task => new TaskDto
                {
                    SectionId = task.SectionId,
                    Id = task.Id,
                    AssignedTAId = task.AssignedTAId,
                    Description = task.Description,
                    CourseRef = task.Section.Course.CourseRef,
                    Assigner = task.Section.Instructor.UserName,
                    Status = task.IsCompleted,
                    TotalHours = task.TaskHours,
                    SectionName = task.Section.Name,
                    InsructorName = task.Section.Instructor.UserName
                }).ToList(),
            })
            .ToListAsync();

        return workRecordsList;
    }

    public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new List<WorkRecordDto>();
        }

        var workRecordsList = await _context.WorkRecord
            .Where(w => w.AssignedTA.DepartmentId == user.DepartmentId
                    && w.IsApprovedByDean == true
                    && w.IsApprovedByDeanOfGraduates == false)
            .Select(workRecordDto => new WorkRecordDto
            {
                Id = workRecordDto.Id,
                TotalHours = workRecordDto.TotalHours,
                AssignedTA = new TADto
                {
                    Id = workRecordDto.AssignedTAId,
                    UserName = workRecordDto.AssignedTA.UserName,
                    UserEmail = workRecordDto.AssignedTA.UserEmail,
                    UniversityId = workRecordDto.AssignedTA.UniversityId,
                    GPA = workRecordDto.AssignedTA.GPA,
                    DepName = workRecordDto.AssignedTA.Department.Name,
                    School = workRecordDto.AssignedTA.Department.School.Name
                },
                StartDate = workRecordDto.StartDate,
                EndDate = workRecordDto.EndDate,
                WorkRecordDate = workRecordDto.WorkRecordDate,
                Tasks = workRecordDto.Tasks.Select(task => new TaskDto
                {
                    SectionId = task.SectionId,
                    Id = task.Id,
                    AssignedTAId = task.AssignedTAId,
                    Description = task.Description,
                    CourseRef = task.Section.Course.CourseRef,
                    Assigner = task.Section.Instructor.UserName,
                    Status = task.IsCompleted,
                    TotalHours = task.TaskHours,
                    SectionName = task.Section.Name,
                    InsructorName = task.Section.Instructor.UserName
                }).ToList(),
            })
            .ToListAsync();

        return workRecordsList;
    }

    public async Task ApproveWorkRecord(int workRecordId)
    {
        // retrieve the work record by the ID
        var record = await _context.WorkRecord
          .FirstOrDefaultAsync(record => record.Id == workRecordId);

        if (record == null) { return; }

        // change the work record’s approval status 
        record.IsApprovedByDean = true;
        record.Status = WorkRecordStatusEnum.IsApprovedByDean;
        await _context.SaveChangesAsync(default);
    }

    public async Task RejectWorkRecord(int workRecordId, string note)
    {
        // retrieve the work record by the ID
        var record = await _context.WorkRecord
          .FirstOrDefaultAsync(record => record.Id == workRecordId);

        if (record == null) { return; }

        // change the work record’s approval status and add the rejection note
        record.IsApprovedByDean = false;
        record.Status = WorkRecordStatusEnum.IsRejectedByDean;
        record.DeanNote = note;
        await _context.SaveChangesAsync(default);
    }

}
