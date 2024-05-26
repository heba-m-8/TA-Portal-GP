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
using TAManagment.Domain.Entities.Identity;
using TAManagment.Domain.Enums;
using static System.Collections.Specialized.BitVector32;

namespace TAManagment.Infrastructure.Services;
public class InstructorService : IInstructorService
{
    private readonly IApplicationDbContext _context;

    public InstructorService(IApplicationDbContext context)
    {
        _context = context;
    }


    //allows an instructor to see all details of their courses' sections
    public async Task<List<SectionDto>> GetInstructorSections(int userId)           
    {
        // Find the sections where the course's instructor id matches the instructor's id
        var sections = await _context.Section
                .Where(section => section.InstructorId == userId)
                .Select(section => new SectionDto
                {
                    Id = section.Id,
                    Name = section.Course.Name,
                    StartTime = section.StartTime,
                    EndTime = section.EndTime,
                    CourseId = section.CourseId,
                    InstructorId = section.InstructorId,
                    TAId = section.TAId,
                    TA = section.TA.UserName,
                    CourseRef = section.Course.CourseRef,
                })
                .ToListAsync();

        return sections;
    }


    //allows instructor to see their TA details
    public async Task<TADto> GetTAsDetails(int sectionId)       
    {
        var section = await _context.Section.FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
            return new TADto();

        var TA = await _context.User
            .Where(TA => TA.Id == section.TAId)
            .Select(TA => new TADto
            {
                UserName = TA.UserName,
                UserEmail = TA.UserEmail,
                UniversityId = TA.UniversityId,
                GPA = TA.GPA
            })
            .FirstOrDefaultAsync();

        if (TA == null)
            return new TADto();
        
        return TA;
    }


    public async Task<List<TaskDto>> GetSectionTasks(int sectionId)
    {
        var section = await _context.Section.FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
            return new List<TaskDto>();

        var tasks = await _context.Tasks
          .Where(task => task.SectionId == section.Id)
          .Select(task => new TaskDto
          {
              //Id = task.Id,
              Description = task.Description,
              SectionId = task.SectionId,
              AssignedTAId = task.AssignedTAId,
              Status = task.IsCompleted
              
          })
          .ToListAsync();

        return tasks;
    }
    

    //allows an instructor to assign tasks to their assigned TAs
    public async Task AssignTask(TaskDto taskDto)           
    {
        //create new task object and set its attributes
        var task = new TAManagment.Domain.Entities.Tasks();
        task.Description = taskDto.Description;
        task.AssignedTAId = taskDto.AssignedTAId;
        task.SectionId = taskDto.SectionId;

        //add task to database and save changes
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync(default);
    }


    public async Task<List<WorkRecordDto>> GetWorkRecords(int userId)
    {
        // get all the instructor's associated work records IDs
        var workRecordsIdList = await _context.WorkRecordInstructorApprover
            .Where(workRecord => workRecord.InstructorId == userId)
            .Select(workRecord => workRecord.WorkRecordId)
            .ToListAsync();

        // get all WRs that have an ID in the workRecordsIdList
        var workRecordsList = await _context.WorkRecord
            .Where(w => workRecordsIdList.Contains(w.Id)
                    && w.IsApprovedByInstructor == null)
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

        //var workRecordsList = await _context.WorkRecord
        //    .Where(w => w.AssignedTA.DepartmentId == user.DepartmentId
        //            && w.IsApprovedByInstructor == true
        //            && w.IsApprovedByHod == false)
        //    .Select(workRecordDto => new WorkRecordDto
        //    {
        //        Id = workRecordDto.Id,
        //        TotalHours = workRecordDto.TotalHours,
        //        AssignedTA = new TADto
        //        {
        //            Id = workRecordDto.AssignedTAId,
        //            UserName = workRecordDto.AssignedTA.UserName,
        //            UserEmail = workRecordDto.AssignedTA.UserEmail,
        //            UniversityId = workRecordDto.AssignedTA.UniversityId,
        //            GPA = workRecordDto.AssignedTA.GPA,
        //            DepName = workRecordDto.AssignedTA.Department.Name,
        //            School = workRecordDto.AssignedTA.Department.School.Name
        //        },
        //        StartDate = workRecordDto.StartDate,
        //        EndDate = workRecordDto.EndDate,
        //        WorkRecordDate = workRecordDto.WorkRecordDate,
        //        Tasks = workRecordDto.Tasks.Select(task => new TaskDto
        //        {
        //            SectionId = task.SectionId,
        //            Id = task.Id,
        //            AssignedTAId = task.AssignedTAId,
        //            Description = task.Description,
        //            CourseRef = task.Section.Course.CourseRef,
        //            Assigner = task.Section.Instructor.UserName,
        //            Status = task.IsCompleted,
        //            TotalHours = task.TaskHours,
        //            SectionName = task.Section.Name,
        //            InsructorName = task.Section.Instructor.UserName
        //        }).ToList(),
        //    })
        //    .ToListAsync();

        // get all the instructor's associated work records IDs
        var workRecordsIdList = await _context.WorkRecordInstructorApprover
            .Where(workRecord => workRecord.InstructorId == userId)
            .Select(workRecord => workRecord.WorkRecordId)
            .ToListAsync();

        // get all WRs that have an ID in the workRecordsIdList
        var workRecordsList = await _context.WorkRecord
            .Where(w => workRecordsIdList.Contains(w.Id)
                    && w.IsApprovedByInstructor == true
                    && w.IsApprovedByHod == false)
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


    public async Task ApproveWorkRecord(int userId, int workRecordId)
    {
        // get the specific work record associated with the given instructor and work record ID
        var workRecord = await _context.WorkRecordInstructorApprover
            .Where(w => w.InstructorId == userId 
                && w.WorkRecordId == workRecordId)
            .FirstOrDefaultAsync();

        if (workRecord == null) { return; }

        // approve it
        workRecord.IsApproved = true;
        await _context.SaveChangesAsync(default);


        // get all similar records
        var similarRecords = await _context.WorkRecordInstructorApprover
           .Where(w => w.WorkRecordId == workRecordId 
                && w.IsApproved != true)
           .ToListAsync();


        // if no similar records are found, update main status
        if(similarRecords.Count < 1)
        {
            // get the main work record
            var record = await _context.WorkRecord
              .FirstOrDefaultAsync(record => record.Id == workRecordId);

            if (record == null) { return; } 

            // change its instructor approval status
            record.IsApprovedByInstructor = true;
            record.Status = WorkRecordStatusEnum.IsApprovedByInstructor;
            await _context.SaveChangesAsync(default);
        }
    }


    public async Task RejectWorkRecord(int userId, int workRecordId, string note)
    {
        // get all the instructor's associated work record
        var workRecord = await _context.WorkRecordInstructorApprover
            .Where(w => w.InstructorId == userId 
                && w.WorkRecordId == workRecordId)
            .FirstOrDefaultAsync();

        if (workRecord == null) { return; }

        workRecord.IsApproved = false;
        await _context.SaveChangesAsync(default);

        // get the record from the workRecord class based on ID 
        var record = await _context.WorkRecord
            .FirstOrDefaultAsync(record => record.Id == workRecordId);

        if(record == null) { return; }

        record.IsApprovedByInstructor = false;
        record.Status = WorkRecordStatusEnum.IsRejectedByInstructor;
        record.InstructorNote = note;
        await _context.SaveChangesAsync(default);
    }
}







