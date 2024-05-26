using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Constants;
using TAManagment.Domain.Entities;
using TAManagment.Domain.Entities.Identity;
using TAManagment.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TAManagment.Infrastructure.Services;
public class HODService : IHODService
{
    private readonly IApplicationDbContext _context;

    public HODService(IApplicationDbContext context)
    {
        _context = context;
    }


    //allows HOD to see all courses' sections in their department 
    public async Task<List<SectionDto>> GetAllSections(int userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            //if user not found, return empty list
            return new List<SectionDto>();
        }

        // Find the sections where the departmentId matches the HOD's departmentId
        var sections = await _context.Section
            .Where(section => section.Course.DepartmentId == user.DepartmentId)
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
                CourseRef=section.Course.CourseRef,
                InstructorName=section.Instructor.UserName
                })
          .ToListAsync();

      return sections;
    }


    //allows HOD to see TA name in drop down menu
    public async Task<List<TADto>> GetTAsNamesIds(int userId, int sectionId)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        var section = await _context.Section.FirstOrDefaultAsync(s=>s.Id ==sectionId);

        if (user == null)
        {
            //if user not found, return empty list
            return new List<TADto>();
        }

        var sectionStartTime = await _context.Section
           .Where(s => s.Id == sectionId)
           .Select(s => s.StartTime)
           .FirstOrDefaultAsync();

        // Filter Lab TAs (phd TAs with role 2) who have a scheduling conflict, 
        // so that they don't get displayed in the drop down menu
        var TAs = await _context.User
           .Where(TA => TA.DepartmentId == user.DepartmentId 
                &&
                (
                ( TA.RoleId==2 
                && (section != null ?section.InstructorId ==null: true)
                && !_context.Section.Any
                    (s => s.TAId == TA.Id           //only check time for this TA's sections
                    && s.StartTime == sectionStartTime
                    && s.Id != sectionId)   //Preven it from identifying conflict with itself
                    
                    )
               || (TA.RoleId == 1 && (section != null ? section.InstructorId != null : true)))
               
               )
           .Select(TA => new TADto
            {
                Id = TA.Id,
                UserName = TA.UserName
            })
           .ToListAsync();

        return TAs;
    }


    //allows HOD to see all the details of TAs
    public async Task<List<TADto>> GetTAsDetails(int userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            //if user not found, return empty list
            return new List<TADto>();
        }

        // Find the TAs whose departmentId matches the HOD's departmentId
        var TAs = await _context.User
            .Where(TA => TA.DepartmentId == user.DepartmentId 
                && (TA.RoleId == 1 || TA.RoleId ==2))
            .Select(TA => new TADto
            {
                Id = TA.Id,
                UserName = TA.UserName,
                UserEmail = TA.UserEmail,
                UniversityId = TA.UniversityId,
                GPA = TA.GPA
            })
            .ToListAsync();

        return TAs;
    }


    //allows HOD to add, remove or change a TA from a section
    public async Task<TADto> ManageTAs(ManageTADto manageTADto)
    {
        // Find the section by its ID
        var section = await _context.Section.Include(x=>x.Course)
            .FirstOrDefaultAsync(s => s.Id == manageTADto.SectionId);

        if (section == null || section.Course == null)
        {
            return new TADto();
        }

        // Find the TA by their ID and project to TADto
        var updatedTA = await _context.User
              .Where(user => user.Id == manageTADto.TaId)
              .Select(user => new TADto
              {
                  Id = user.Id,
                  UserName = user.UserName,
                  DepartmentId = user.DepartmentId,
                  RoleId= user.RoleId,
              })
              .FirstOrDefaultAsync();

        if (updatedTA == null)
        {
            return new TADto();
        }

        // Check if the TA's department matches the section's department
        if (section.Course.DepartmentId == updatedTA.DepartmentId)
        {
            // Assign the TA to the section
            section.TAId = manageTADto.TaId;
            if (updatedTA.RoleId == (int)RoleEnum.TAPHD)
             section.InstructorId = manageTADto.TaId;

            await _context.SaveChangesAsync(default);

            return updatedTA;
        }

        // Return an empty TADto if the department IDs do not match
        return new TADto();
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
                && w.IsApprovedByInstructor == true
                && w.IsApprovedByHod == null)
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
                    && w.IsApprovedByHod == true
                    && w.IsApprovedByDean == false)
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
            var record = await _context.WorkRecord
              .FirstOrDefaultAsync(record => record.Id == workRecordId);

            if (record == null) { return; }

            record.IsApprovedByHod = true;
            record.Status = WorkRecordStatusEnum.IsApprovedByHod;
            await _context.SaveChangesAsync(default);
    }

    public async Task RejectWorkRecord(int workRecordId, string note)
    {
            var record = await _context.WorkRecord
              .FirstOrDefaultAsync(record => record.Id == workRecordId);

            if (record == null) { return; }

            record.IsApprovedByHod = false;
            record.Status = WorkRecordStatusEnum.IsRejectedByHod;
            record.HODnote = note;
            await _context.SaveChangesAsync(default);
    }

}






