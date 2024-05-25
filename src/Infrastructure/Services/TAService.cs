using Microsoft.EntityFrameworkCore;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Entities;
using TAManagment.Domain.Enums;

namespace TAManagment.Infrastructure.Services;
public class TAService : ITAService
{
    private readonly IApplicationDbContext _context;

    public TAService (IApplicationDbContext context)
    {
        _context = context;
    }


    //allows TA to see all the sections they are assigned to
    public async Task<List<SectionDto>> GetTASections(int userId)
    {
        // Find the sections where the assigned TA ID matches the TA's ID
        var sections = await _context.Section
            .Where(section => section.TAId == userId)
            .Select(section => new SectionDto
            {
                Id = section.Id,
                Name = section.Course.Name,
                StartTime = section.StartTime,
                EndTime = section.EndTime,
                CourseId = section.CourseId,
                InstructorId = section.InstructorId,
                CourseRef = section.Course.CourseRef,
                InstructorName = section.Instructor.UserName
            })
          .ToListAsync();

        return sections;
    }


    //allows TA to see all their uncompleted tasks
    public async Task<List<TaskDto>> GetTATasks(int userId , bool status = false)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            //if user not found, return empty list
            return new List<TaskDto>();
        }

        // Find the tasks where the assigned TA ID matches the TA's ID
        var tasks = await _context.Tasks
            .Where(task => task.AssignedTAId == user.Id 
                && task.IsCompleted == status)
            .Select(task => new TaskDto
            {
                Id = task.Id,
                Description = task.Description,
                CourseRef = task.Section.Course.CourseRef,
                Assigner = task.Section.Instructor.UserName,
                Status = task.IsCompleted,
                TotalHours = task.TaskHours,
                SectionName = task.Section.Course.Name,
            })
          .ToListAsync();

        return tasks;
    }


    //allows TA to update a task's status (if it has been completed)
    public async Task<TaskDto> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        // Retrieve the task to be updated using the ID
         var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == updateTaskDto.Id);
         var currentDate = DateTime.UtcNow;

        // Calculate the start and end dates of the current month
        var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
         var endDate = new DateTime(currentDate.Year, currentDate.Month, 28);

        if (task == null)
        {
            return new TaskDto();
        }
 
        // Update task status and hours 
        task.IsCompleted = true;
        task.TaskHours = updateTaskDto.TotalHours;
        await _context.SaveChangesAsync(default);

        // Find the work record for the current month that matches the task's TA ID
        var workRecord = await _context.WorkRecord
        .FirstOrDefaultAsync(w => w.WorkRecordDate.Month == currentDate.Month 
                            && w.WorkRecordDate.Year == currentDate.Year
                            && w.AssignedTAId == task.AssignedTAId);

        //if work record exists, add task to it
        if (workRecord != null)
        {
            workRecord.Tasks.Add(task);
            workRecord.TotalHours = workRecord.TotalHours + task.TaskHours;
            await _context.SaveChangesAsync(default);
        }

        //if it does not exist, create it and add the task to it
        else
        {
            WorkRecord newWorkRecord = new WorkRecord();
            newWorkRecord.AssignedTAId= task.AssignedTAId;
            newWorkRecord.StartDate = startDate;
            newWorkRecord.EndDate = endDate;
            newWorkRecord.WorkRecordDate = currentDate;
            newWorkRecord.TotalHours = updateTaskDto.TotalHours;
            newWorkRecord.Tasks.Add(task);

        await _context.WorkRecord.AddAsync(newWorkRecord);
        await _context.SaveChangesAsync(default);
        }

        //return the updated task
        return new TaskDto
        {
            Id = task.Id,
            Status = task.IsCompleted
        };
    }


    public async Task<List<WorkRecordDto>> GetWorkRecord(int userId, bool IsSubmitted=false)
    {
        var workRecordList = await _context.WorkRecord
             .Where(record => record.AssignedTAId == userId && record.IsSubmitted == IsSubmitted)
             .Select(record => new WorkRecordDto
             {
                 Id = record.Id,
                 TotalHours = record.TotalHours,
                 AssignedTA = new TADto
                 {
                     Id= userId,
                     UserName = record.AssignedTA.UserName,
                     UserEmail = record.AssignedTA.UserEmail,
                     UniversityId = record.AssignedTA.UniversityId,
                     GPA = record.AssignedTA.GPA,
                     DepName = record.AssignedTA.Department.Name,
                     School = record.AssignedTA.Department.School.Name
                 },
                 StartDate = record.StartDate,
                 EndDate = record.EndDate,
                 WorkRecordDate = record.WorkRecordDate,
                 Tasks = record.Tasks.Select(task => new TaskDto
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

        return workRecordList;
    }


    // submits a work record and stores it in WorkRecordInstructorApprover
    public async Task SubmitWorkRecord(UpdateWorkRecordDto updateWorkRecordDto)
    {
        var workRecord = await _context.WorkRecord.FirstOrDefaultAsync(w => w.Id == updateWorkRecordDto.Id);
      
        if (workRecord == null) {   return;  }

        workRecord.IsSubmitted = true;
        workRecord.Status = WorkRecordStatusEnum.IsSubmitted;

        // get the TA's assigned sections based on WR's TA ID

        var sections = await _context.Section
             .Where(section => section.TAId == workRecord.AssignedTAId)
             .Select(section => new SectionDto
             {
                 Id = section.Id,
                 Name = section.Name,
                 InstructorId = section.InstructorId
             })
             .ToListAsync();

         sections = sections.DistinctBy(x => x.InstructorId).ToList();




        // add records to WorkRecordInstructorApprover for each instructor 
        foreach (var section in sections)
        {

            var approvedWorkRecord = new WorkRecordInstructorApprover();
            approvedWorkRecord.WorkRecordId = workRecord.Id;
            approvedWorkRecord.InstructorId = section.InstructorId;

            await _context.WorkRecordInstructorApprover.AddAsync(approvedWorkRecord);
            await _context.SaveChangesAsync(default);
        }
    }


    public async Task<TaskDto> UpdateTaskHours(UpdateTaskDto updateTaskDto)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == updateTaskDto.Id);

        if (task == null)
        {
            return new TaskDto();
        }

        task.TaskHours = updateTaskDto.TotalHours;
        await _context.SaveChangesAsync(default);


        var workRecord = await _context.WorkRecord.FirstOrDefaultAsync(w => w.Id == task.WorkRecordId);

        if(workRecord == null)
            return new TaskDto();

        workRecord.TotalHours = workRecord.TotalHours - updateTaskDto.OldTotalHours + updateTaskDto.TotalHours;
        await _context.SaveChangesAsync(default);

        //return the updated task
        return new TaskDto
        {
            Id = task.Id,
            Status = task.IsCompleted
        };
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
        await ResetInstructorApproversApprovalStatus(updateWorkRecordDto.Id);
    }

    private void ResetApprovalStatus(WorkRecord workRecord)
    {
        // Reset all approval flags to null
        workRecord.IsApprovedByInstructor = null;
        workRecord.IsApprovedByHod = null;
        workRecord.IsApprovedByDean = null;
        workRecord.IsApprovedByDeanOfGraduates = null;
        workRecord.IsApprovedByFinance = null;
    }

    private async Task ResetInstructorApproversApprovalStatus(int workRecordId)
    {
        // Fetch all instructor approvers associated with the work record
        var instructorApprovers = await _context.WorkRecordInstructorApprover
            .Where(x => x.WorkRecordId == workRecordId)
            .ToListAsync();

        // Reset approval status for each instructor approver
        foreach (var approver in instructorApprovers)
        {
            approver.IsApproved = null;
        }

        // Save changes to the database
        await _context.SaveChangesAsync(default);
    }


}
