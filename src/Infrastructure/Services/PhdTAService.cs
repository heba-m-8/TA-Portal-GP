using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Entities;
using TAManagment.Domain.Enums;

namespace TAManagment.Infrastructure.Services;
public class PhdTAService : IPhdTAService
{
    private readonly IApplicationDbContext _context;

    public PhdTAService(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task CreateTask(PhdTaskDto phdTaskDto)
    {
        // create Task
        var phdTask = new Domain.Entities.Tasks();
        phdTask.Description = phdTaskDto.Description;
        phdTask.SectionId = phdTaskDto.SectionId;
        phdTask.IsCompleted = phdTaskDto.Status;
        phdTask.TaskHours = phdTaskDto.Hours;
        phdTask.AssignedTAId = phdTaskDto.AssignedTAId;

        await _context.Tasks.AddAsync(phdTask);
        await _context.SaveChangesAsync(default);
        
        
        var currentDate = DateTime.UtcNow;

        // Calculate the start and end dates of the current month
        var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
        var endDate = new DateTime(currentDate.Year, currentDate.Month, 28);

        if (phdTask == null)
        {
            return ;
        }


        // Find the work record for the current month that matches the task's TA ID
        var workRecord = await _context.WorkRecord
        .FirstOrDefaultAsync(w => w.WorkRecordDate.Month == currentDate.Month
                            && w.WorkRecordDate.Year == currentDate.Year
                            && w.AssignedTAId == phdTask.AssignedTAId);

        //if work record exists, add task to it
        if (workRecord != null)
        {
            workRecord.Tasks.Add(phdTask);
            workRecord.TotalHours = workRecord.TotalHours + phdTask.TaskHours;
            await _context.SaveChangesAsync(default);
        }

        //if it does not exist, create it and add the task to it
        else
        {
            WorkRecord newWorkRecord = new WorkRecord();
            newWorkRecord.AssignedTAId = phdTask.AssignedTAId;
            newWorkRecord.StartDate = startDate;
            newWorkRecord.EndDate = endDate;
            newWorkRecord.WorkRecordDate = currentDate;
            newWorkRecord.TotalHours = phdTask.TaskHours;
            newWorkRecord.Tasks.Add(phdTask);

            await _context.WorkRecord.AddAsync(newWorkRecord);
            await _context.SaveChangesAsync(default);
        }
    }



    public async Task<List<SectionDto>> GetPhdTASections(int userId)
    {
        // Find the sections where the instructor ID is the user ID
        var sections = await _context.Section
            .Where(section => section.InstructorId == userId)
            .Select(section => new SectionDto
            {
                Id = section.Id,
                Name = section.Course.Name,
                StartTime = section.StartTime,
                EndTime = section.EndTime,
                CourseId = section.CourseId,
                CourseRef = section.Course.CourseRef,
            })
          .ToListAsync();

        return sections;
    }

    //public async Task<List<WorkRecordDto>> GetRejectedWorkRecords(int userId)
    //{
    //    var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

    //    if (user == null)
    //    {
    //        return new List<WorkRecordDto>();
    //    }

    //    var workRecordsList = await _context.WorkRecord
    //        .Where(w => w.AssignedTA.DepartmentId == user.DepartmentId
    //                && w.IsApprovedByInstructor == true
    //                && w.IsApprovedByHod == false)
    //        .Select(workRecordDto => new WorkRecordDto
    //        {
    //            Id = workRecordDto.Id,
    //            TotalHours = workRecordDto.TotalHours,
    //            AssignedTA = new TADto
    //            {
    //                Id = workRecordDto.AssignedTAId,
    //                UserName = workRecordDto.AssignedTA.UserName,
    //                UserEmail = workRecordDto.AssignedTA.UserEmail,
    //                UniversityId = workRecordDto.AssignedTA.UniversityId,
    //                GPA = workRecordDto.AssignedTA.GPA,
    //                DepName = workRecordDto.AssignedTA.Department.Name,
    //                School = workRecordDto.AssignedTA.Department.School.Name
    //            },
    //            StartDate = workRecordDto.StartDate,
    //            EndDate = workRecordDto.EndDate,
    //            WorkRecordDate = workRecordDto.WorkRecordDate,
    //            Tasks = workRecordDto.Tasks.Select(task => new TaskDto
    //            {
    //                SectionId = task.SectionId,
    //                Id = task.Id,
    //                AssignedTAId = task.AssignedTAId,
    //                Description = task.Description,
    //                CourseRef = task.Section.Course.CourseRef,
    //                Assigner = task.Section.Instructor.UserName,
    //                Status = task.IsCompleted,
    //                TotalHours = task.TaskHours,
    //                SectionName = task.Section.Name,
    //                InsructorName = task.Section.Instructor.UserName
    //            }).ToList(),
    //        })
    //        .ToListAsync();

    //    return workRecordsList;
    //}

    public async Task SubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto)
    {
        var workRecord = await _context.WorkRecord.FirstOrDefaultAsync(w => w.Id == updateWorkRecordDto.Id);

        if (workRecord == null) { return; }

        workRecord.IsSubmitted = true;
        workRecord.Status = WorkRecordStatusEnum.IsSubmitted;
        workRecord.IsApprovedByInstructor= true;
        await _context.SaveChangesAsync(default);   
    }



    public async Task ReSubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto)
    {
        // Fetch the work record from the database
        var workRecord = await _context.WorkRecord.FirstOrDefaultAsync(w => w.Id == updateWorkRecordDto.Id);

        // If the work record doesn't exist, return early
        if (workRecord == null)
        {
            return;
        }

        // Update work record properties
        workRecord.IsSubmitted = true;
        workRecord.Status = WorkRecordStatusEnum.IsReSubmitted;
        ResetApprovalStatus(workRecord);

        // Save changes to the database
        await _context.SaveChangesAsync(default);

        // Reset approval status for associated instructor approvers
    }

    private void ResetApprovalStatus(WorkRecord workRecord)
    {
        // Reset all approval flags to null
        workRecord.IsApprovedByHod = null;
        workRecord.IsApprovedByDean = null;
        workRecord.IsApprovedByDeanOfGraduates = null;
        workRecord.IsApprovedByFinance = null;
    }




}
