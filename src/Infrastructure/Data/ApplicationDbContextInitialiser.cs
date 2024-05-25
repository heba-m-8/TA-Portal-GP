using System.Runtime.InteropServices;
using TAManagment.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TAManagment.Domain.Entities.Identity;
using TAManagment.Domain.Entities;

namespace TAManagment.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
     

        await SeedSchoolDataAsync();

    }

    private async Task SeedSchoolDataAsync()
    {
        // Check if schools exist, if not, seed them
        if (!_context.School.Any())
        {
            var schools = new List<School>
        {
            new School { Name = "King Hussein School of Computing Sciences" },
            new School { Name = "King Abdullah II School of Engineering" },
            new School { Name = "King Talal School of Business Technology" }
        };

            await _context.School.AddRangeAsync(schools);
            await _context.SaveChangesAsync();

            // Seed departments
            var departments = new List<Department>
        {
            // Departments for King Hussein School of Computing Sciences
            new Department { Name = "Computer Science Department", SchoolId = schools[0].Id },
            new Department { Name = "Computer Graphics and Animation Department", SchoolId = schools[0].Id },
            new Department { Name = "Software Engineering Department", SchoolId = schools[0].Id },
            new Department { Name = "Data Science Department", SchoolId = schools[0].Id },
            new Department { Name = "Cyber Security Department", SchoolId = schools[0].Id },

            // Departments for King Talal School of Business Technology
            new Department { Name = "Business Administration Department", SchoolId = schools[2].Id },
            new Department { Name = "Accounting Department", SchoolId = schools[2].Id },
            new Department { Name = "Business Information Technology Department", SchoolId = schools[2].Id },
            new Department { Name = "E-Marketing and Social Media Department", SchoolId = schools[2].Id },

            // Departments for King Abdullah II School of Engineering
            new Department { Name = "Electrical Engineering Department", SchoolId = schools[1].Id },
            new Department { Name = "Communications Engineering Department", SchoolId = schools[1].Id },
            new Department { Name = "Computer Engineering Department", SchoolId = schools[1].Id }
        };

            await _context.Department.AddRangeAsync(departments);
            await _context.SaveChangesAsync();

            // Seed courses
            var courses = new List<Course>
            {
                // Courses
                new Course { Name = "Structured Programming", DepartmentId = departments[0].Id, CourseRef = 11103 },
                new Course { Name = "Structured Programming Lab", DepartmentId = departments[0].Id, CourseRef = 11151 },
                new Course { Name = "Object Oriented Programming", DepartmentId = departments[0].Id, CourseRef = 11206 },
                new Course { Name = "Object Oriented Programming Lab", DepartmentId = departments[0].Id, CourseRef = 11253 },
                new Course { Name = "Data Structures and Introduction to Algorithms", DepartmentId = departments[0].Id, CourseRef = 11212 },
                new Course { Name = "Database Systems", DepartmentId = departments[0].Id, CourseRef = 11323 },

                new Course { Name = "Webpage Design and Internet programming", DepartmentId = departments[1].Id, CourseRef = 12243 },
                new Course { Name = "Webpage Design and Internet programming Lab", DepartmentId = departments[1].Id, CourseRef = 12242 },
                new Course { Name = "Multimedia Systems", DepartmentId = departments[1].Id, CourseRef = 12348 },

                new Course { Name = "Mobile Application Development", DepartmentId = departments[2].Id, CourseRef = 13334 },
                new Course { Name = "Server Side Programming", DepartmentId = departments[2].Id, CourseRef = 13335 },
                new Course { Name = "Advanced Topics in Programming", DepartmentId = departments[2].Id, CourseRef = 13431 },

                new Course { Name = "Computer Architecture for Machine Learning", DepartmentId = departments[3].Id, CourseRef = 14350 },
                new Course { Name = "Natural Language Processing", DepartmentId = departments[3].Id, CourseRef = 14351 },
                new Course { Name = "Data Visualization", DepartmentId = departments[3].Id, CourseRef = 14364 },

                new Course { Name = "Database Security", DepartmentId = departments[4].Id, CourseRef = 15360 },
                new Course { Name = "Mobile and Wireless Security", DepartmentId = departments[4].Id, CourseRef = 15420 },
                new Course { Name = "Hacking Techniques and Intrusion Detection", DepartmentId = departments[4].Id, CourseRef = 15460 },

                new Course { Name = "Human Resources Management", DepartmentId = departments[5].Id, CourseRef = 33405 },
                new Course { Name = "Quality Management", DepartmentId = departments[5].Id, CourseRef = 33317 },
                new Course { Name = "Logistics Management", DepartmentId = departments[5].Id, CourseRef = 33218 },

                new Course { Name = "Governmental Accounting", DepartmentId = departments[6].Id, CourseRef = 34233 },
                new Course { Name = "Advanced Accounting", DepartmentId = departments[6].Id, CourseRef = 34307 },
                new Course { Name = "Managerial Accounting", DepartmentId = departments[6].Id, CourseRef = 34321 },

                new Course { Name = "E-Business for Business Students", DepartmentId = departments[7].Id, CourseRef = 36232 },
                new Course { Name = "Advanced Business Applications Programming", DepartmentId = departments[7].Id, CourseRef = 36312 },
                new Course { Name = "Business Intelligence Systems", DepartmentId = departments[7].Id, CourseRef = 36404 },

                new Course { Name = "E. Marketing Channels", DepartmentId = departments[8].Id, CourseRef = 35314 },
                new Course { Name = "Advertising Technology", DepartmentId = departments[8].Id, CourseRef = 35326 },
                new Course { Name = "Modern E-marketing Topics", DepartmentId = departments[8].Id, CourseRef = 35435 },

                new Course { Name = "Digital Logic Design", DepartmentId = departments[9].Id, CourseRef = 22241 },
                //new Course { Name = "Embedded Systems", DepartmentId = departments[9].Id, CourseRef = 22442 },
                new Course { Name = "Electric Circuits (1)", DepartmentId = departments[9].Id, CourseRef = 21221 },

                new Course { Name = "Communication Principles", DepartmentId = departments[10].Id, CourseRef = 23355 },
                new Course { Name = "Cellular Communications", DepartmentId = departments[10].Id, CourseRef = 23457 },
                new Course { Name = "Introduction to IoT", DepartmentId = departments[10].Id, CourseRef = 27251 },

                new Course { Name = "Computer Architecture (1)", DepartmentId = departments[11].Id, CourseRef = 22320 },
                new Course { Name = "Digital Electronics", DepartmentId = departments[11].Id, CourseRef = 21322 },
                new Course { Name = "Digital Electronics Lab", DepartmentId = departments[11].Id, CourseRef = 21339 }
            };

            await _context.Course.AddRangeAsync(courses);
            await _context.SaveChangesAsync();

            if (!_context.Role.Any())
            {
                var roles = new List<Role>
                {
                    new Role {  RoleName = "TA" },
                    new Role {  RoleName = "TAPHD" },
                    new Role {  RoleName = "Instructor" },
                    new Role {  RoleName = "HOD" },
                    new Role {  RoleName = "Dean" },
                    new Role {  RoleName = "DeanOfGraduateStudies" },
                    new Role {  RoleName = "Finance" }
                };

                await _context.Role.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }


            if (!_context.Users.Any())
            {
                var users = new List<Users>
            {
                // TAs
                new Users { UserName = "John Doe", UserEmail = "john.doe.20201234@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20201234", IsActive = false, DepartmentId = 1, RoleId = 1,GPA=80 },
                new Users { UserName = "Alice Smith", UserEmail = "alice.smith.20201235@example.com", UserPassword = "SecurePass123", UniversityId = "20201235", IsActive = false, DepartmentId = 1, RoleId = 1,GPA=81 },
                new Users { UserName = "Michael Johnson", UserEmail = "michael.johnson.20201236@example.com", UserPassword = "StrongPass456", UniversityId = "20201236", IsActive = false, DepartmentId = 2, RoleId = 1,GPA=82 },
                new Users { UserName = "Emily Brown", UserEmail = "emily.brown.20201237@example.com", UserPassword = "SafePassword789", UniversityId = "20201237", IsActive = false, DepartmentId = 2, RoleId = 1,GPA=83 },
                new Users { UserName = "David Wilson", UserEmail = "david.wilson.20201238@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20201238", IsActive = false, DepartmentId = 3, RoleId = 1,GPA=84 },
                new Users { UserName = "Sophia Anderson", UserEmail = "sophia.anderson.20201239@example.com", UserPassword = "SecurePass123", UniversityId = "20201239", IsActive = false, DepartmentId = 3, RoleId = 1,GPA=85 },
                new Users { UserName = "James Martinez", UserEmail = "james.martinez.20201240@example.com", UserPassword = "StrongPass456", UniversityId = "20201240", IsActive = false, DepartmentId = 4, RoleId = 1,GPA=86 },
                new Users { UserName = "Emma Taylor", UserEmail = "emma.taylor.20201241@example.com", UserPassword = "SafePassword789", UniversityId = "20201241", IsActive = false, DepartmentId = 4, RoleId = 1 ,GPA=87},
                new Users { UserName = "Daniel Thomas", UserEmail = "daniel.thomas.20201242@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20201242", IsActive = false, DepartmentId = 5, RoleId = 1 ,GPA=88},
                new Users { UserName = "Olivia White", UserEmail = "olivia.white.20201243@example.com", UserPassword = "SecurePass123", UniversityId = "20201243", IsActive = false, DepartmentId = 5, RoleId = 1,GPA=89 },
                new Users { UserName = "Matthew Harris", UserEmail = "matthew.harris.20211234@example.com", UserPassword = "StrongPass456", UniversityId = "20211234", IsActive = false, DepartmentId = 6, RoleId = 1,GPA=90 },
                new Users { UserName = "Ava Martin", UserEmail = "ava.martin.20211235@example.com", UserPassword = "SafePassword789", UniversityId = "20211235", IsActive = false, DepartmentId = 6, RoleId = 1 ,GPA=81},
                new Users { UserName = "Ethan Thompson", UserEmail = "ethan.thompson.20211236@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20211236", IsActive = false, DepartmentId = 7, RoleId = 1,GPA=82 },
                new Users { UserName = "Samantha Garcia", UserEmail = "samantha.garcia.20211237@example.com", UserPassword = "SecurePass123", UniversityId = "20211237", IsActive = false, DepartmentId = 7, RoleId = 1,GPA=83 },
                new Users { UserName = "Isabella Martinez", UserEmail = "isabella.martinez.20211238@example.com", UserPassword = "StrongPass456", UniversityId = "20211238", IsActive = false, DepartmentId = 8, RoleId = 1,GPA=84 },
                new Users { UserName = "Mia Robinson", UserEmail = "mia.robinson.20211239@example.com", UserPassword = "SafePassword789", UniversityId = "20211239", IsActive = false, DepartmentId = 8, RoleId = 1,GPA=85 },
                new Users { UserName = "Alexander Lee", UserEmail = "alexander.lee.20211240@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20211240", IsActive = false, DepartmentId = 9, RoleId = 1 ,GPA=86},
                new Users { UserName = "William Walker", UserEmail = "william.walker.20211241@example.com", UserPassword = "SecurePass123", UniversityId = "20211241", IsActive = false, DepartmentId = 9, RoleId = 1,GPA=87 },
                new Users { UserName = "Aiden Allen", UserEmail = "aiden.allen.20221234@example.com", UserPassword = "StrongPass456", UniversityId = "20221234", IsActive = false, DepartmentId = 10, RoleId = 1 ,GPA=88},
                new Users { UserName = "Grace Young", UserEmail = "grace.young.20221235@example.com", UserPassword = "SafePassword789", UniversityId = "20221235", IsActive = false, DepartmentId = 10, RoleId = 1,GPA=89 },
                new Users { UserName = "Sofia Hall", UserEmail = "sofia.hall.20221236@example.com", UserPassword = "P@ssw0rd123", UniversityId = "20221236", IsActive = false, DepartmentId = 11, RoleId = 1 ,GPA=90},
                
                //PHD TAs
                new Users { UserName = "Sam Martin", UserEmail = "sam.martin.20201244@example.com", UserPassword = "P@hf12", UniversityId = "20201244", IsActive = false, DepartmentId = 1, RoleId = 2,GPA=84 },
                new Users { UserName = "David Brown", UserEmail = "david.brown.20201245@example.com", UserPassword = "P@7262", UniversityId = "20201245", IsActive = false, DepartmentId = 2, RoleId = 2,GPA=81 },
                new Users { UserName = "Charles Shorey", UserEmail = "charles.shorey.20201246@example.com", UserPassword = "H7126_", UniversityId = "20201246", IsActive = false, DepartmentId = 3, RoleId = 2,GPA=81 },
                new Users { UserName = "Cody Anderson", UserEmail = "cody.anderson.20201247@example.com", UserPassword = "98Btu_", UniversityId = "20201247", IsActive = false, DepartmentId = 4, RoleId = 2,GPA=88 },
                new Users { UserName = "Alisha Beckham", UserEmail = "alisha.beckham.20211242@example.com", UserPassword = "872bdr8", UniversityId = "20211242", IsActive = false, DepartmentId = 5, RoleId = 2,GPA=86 },
                new Users { UserName = "Susan Curtis", UserEmail = "susan.curtis.20211243@example.com", UserPassword = "hffw7e9", UniversityId = "20211243", IsActive = false, DepartmentId = 6, RoleId = 2,GPA=85 },
                new Users { UserName = "Jude Sanderson", UserEmail = "jude.sanderson.20211244@example.com", UserPassword = "@8hfkIL", UniversityId = "20211244", IsActive = false, DepartmentId = 7, RoleId = 2,GPA=89 },
                new Users { UserName = "Robert Watkins", UserEmail = "robert.watkins.20211245@example.com", UserPassword = "_pDJFE@", UniversityId = "20211245", IsActive = false, DepartmentId = 8, RoleId = 2,GPA=87 },
                new Users { UserName = "Reece Sanderson", UserEmail = "reece.sanderson.20221237@example.com", UserPassword = "JhsaO_", UniversityId = "20221237", IsActive = false, DepartmentId = 9, RoleId = 2,GPA=91 },
                new Users { UserName = "Nicholas Newman", UserEmail = "nicholas.newman.20221238@example.com", UserPassword = "HAB_@!", UniversityId = "20221238", IsActive = false, DepartmentId = 10, RoleId = 2,GPA=90 },
                new Users { UserName = "Sebastian Knight", UserEmail = "sebastian.night.20221239@example.com", UserPassword = "hfkf839", UniversityId = "20221239", IsActive = false, DepartmentId = 11, RoleId = 2,GPA=84 },

                
                // Instructors

                new Users { UserName = "Benjamin Clark", UserEmail = "benjamin.clark.1256@example.com", UserPassword = "InstructorPass17", UniversityId = "1256", IsActive = false, DepartmentId = 1, RoleId = 3 },
                new Users { UserName = "Zoe Hall", UserEmail = "zoe.hall.1257@example.com", UserPassword = "InstructorPass18", UniversityId = "1257", IsActive = false, DepartmentId = 1, RoleId = 3 },
                new Users { UserName = "William Allen", UserEmail = "william.allen.1258@example.com", UserPassword = "InstructorPass19", UniversityId = "1258", IsActive = false, DepartmentId = 1, RoleId = 3 },
                new Users { UserName = "Victoria King", UserEmail = "victoria.king.1259@example.com", UserPassword = "InstructorPass20", UniversityId = "1259", IsActive = false, DepartmentId = 1, RoleId = 3 },

                new Users { UserName = "Andy Smith", UserEmail = "andy.smith.1238@example.com", UserPassword = "InstructorDHD__6", UniversityId = "1238", IsActive = false, DepartmentId = 2, RoleId = 3 },
                new Users { UserName = "Theo Young", UserEmail = "theo.young.1239@example.com", UserPassword = "InstructorPJDH_A", UniversityId = "1239", IsActive = false, DepartmentId = 2, RoleId = 3 },

                new Users { UserName = "Christopher Scott", UserEmail = "christopher.scott.1260@example.com", UserPassword = "InstructorPass21", UniversityId = "1260", IsActive = false, DepartmentId = 3, RoleId = 3 },
                new Users { UserName = "Anna Green", UserEmail = "anna.green.1261@example.com", UserPassword = "InstructorPass22", UniversityId = "1261", IsActive = false, DepartmentId = 3, RoleId = 3 },

                new Users { UserName = "Andrew Evans", UserEmail = "andrew.evans.1262@example.com", UserPassword = "InstructorPass23", UniversityId = "1262", IsActive = false, DepartmentId = 4, RoleId = 3 },
                new Users { UserName = "Ella Baker", UserEmail = "ella.baker.1263@example.com", UserPassword = "InstructorPass24", UniversityId = "1263", IsActive = false, DepartmentId = 4, RoleId = 3 },

                new Users { UserName = "Nicholas Carter", UserEmail = "nicholas.carter.1264@example.com", UserPassword = "InstructorPass25", UniversityId = "1264", IsActive = false, DepartmentId = 5, RoleId = 3 },
                new Users { UserName = "Hannah Adams", UserEmail = "hannah.adams.1265@example.com", UserPassword = "InstructorPass26", UniversityId = "1265", IsActive = false, DepartmentId = 5, RoleId = 3 },

                new Users { UserName = "Isaac Parker", UserEmail = "isaac.parker.1266@example.com", UserPassword = "InstructorPass27", UniversityId = "1266", IsActive = false, DepartmentId = 6, RoleId = 3 },
                new Users { UserName = "Avery Wright", UserEmail = "avery.wright.1267@example.com", UserPassword = "InstructorPass28", UniversityId = "1267", IsActive = false, DepartmentId = 6, RoleId = 3 },


                new Users { UserName = "Robert Johnson", UserEmail = "robert.johnson.1240@example.com", UserPassword = "InstructorPass1", UniversityId = "1240", IsActive = false, DepartmentId = 5, RoleId = 3 },
                new Users { UserName = "Emma Williams", UserEmail = "emma.williams.1241@example.com", UserPassword = "InstructorPass2", UniversityId = "1241", IsActive = false, DepartmentId = 5, RoleId = 3 },

                new Users { UserName = "Evelyn Lee", UserEmail = "evelyn.lee.1268@example.com", UserPassword = "InstructorPass29", UniversityId = "1268", IsActive = false, DepartmentId = 7, RoleId = 3 },
                new Users { UserName = "Carter Roberts", UserEmail = "carter.roberts.1269@example.com", UserPassword = "InstructorPass30", UniversityId = "1269", IsActive = false, DepartmentId = 7, RoleId = 3 },


                new Users { UserName = "Michael Thomas", UserEmail = "michael.thomas.1246@example.com", UserPassword = "InstructorPass7", UniversityId = "1246", IsActive = false, DepartmentId = 8, RoleId = 3 },
                new Users { UserName = "Abigail Jackson", UserEmail = "abigail.jackson.1247@example.com", UserPassword = "InstructorPass8", UniversityId = "1247", IsActive = false, DepartmentId = 8, RoleId = 3 },

                new Users { UserName = "James White", UserEmail = "james.white.1248@example.com", UserPassword = "InstructorPass9", UniversityId = "1248", IsActive = false, DepartmentId = 9, RoleId = 3 },
                new Users { UserName = "Sophia Harris", UserEmail = "sophia.harris.1249@example.com", UserPassword = "InstructorPass10", UniversityId = "1249", IsActive = false, DepartmentId = 9, RoleId = 3 },

                new Users { UserName = "Matthew Martin", UserEmail = "matthew.martin.1250@example.com", UserPassword = "InstructorPass11", UniversityId = "1250", IsActive = false, DepartmentId = 10, RoleId = 3 },
                new Users { UserName = "Grace Thompson", UserEmail = "grace.thompson.1251@example.com", UserPassword = "InstructorPass12", UniversityId = "1251", IsActive = false, DepartmentId = 10, RoleId = 3 },

                new Users { UserName = "Daniel Robinson", UserEmail = "daniel.robinson.1252@example.com", UserPassword = "InstructorPass13", UniversityId = "1252", IsActive = false, DepartmentId = 11, RoleId = 3 },
                new Users { UserName = "Lily Lewis", UserEmail = "lily.lewis.1253@example.com", UserPassword = "InstructorPass14", UniversityId = "1253", IsActive = false, DepartmentId = 11, RoleId = 3 },

                new Users { UserName = "Jackson Walker", UserEmail = "jackson.walker.1254@example.com", UserPassword = "InstructorPass15", UniversityId = "1254", IsActive = false, DepartmentId = 12, RoleId = 3 },
                new Users { UserName = "Madison Young", UserEmail = "madison.young.1255@example.com", UserPassword = "InstructorPass16", UniversityId = "1255", IsActive = false, DepartmentId = 12, RoleId = 3 },
                new Users { UserName = "Suzy Willson", UserEmail = "suzy.willson.1230@example.com", UserPassword = "InstructorPass16", UniversityId = "1230", IsActive = false, DepartmentId = 12, RoleId = 3 },

              
              
                // HOD
                new Users { UserName = "Adam Smith", UserEmail = "adam.smith.1340@example.com", UserPassword = "HODPass1", UniversityId = "1340", IsActive = false, DepartmentId = 5, RoleId = 4 },
                new Users { UserName = "Emily Johnson", UserEmail = "emily.johnson.1341@example.com", UserPassword = "HODPass2", UniversityId = "1341", IsActive = false, DepartmentId = 5, RoleId = 4 },
                new Users { UserName = "David Brown", UserEmail = "david.brown.1342@example.com", UserPassword = "HODPass3", UniversityId = "1342", IsActive = false, DepartmentId = 6, RoleId = 4 },
                new Users { UserName = "Ava Jones", UserEmail = "ava.jones.1343@example.com", UserPassword = "HODPass4", UniversityId = "1343", IsActive = false, DepartmentId = 6, RoleId = 4 },
                new Users { UserName = "Joseph Taylor", UserEmail = "joseph.taylor.1344@example.com", UserPassword = "HODPass5", UniversityId = "1344", IsActive = false, DepartmentId = 7, RoleId = 4 },
                new Users { UserName = "Charlotte Anderson", UserEmail = "charlotte.anderson.1345@example.com", UserPassword = "HODPass6", UniversityId = "1345", IsActive = false, DepartmentId = 7, RoleId = 4 },
                new Users { UserName = "Michael Thomas", UserEmail = "michael.thomas.1346@example.com", UserPassword = "HODPass7", UniversityId = "1346", IsActive = false, DepartmentId = 8, RoleId = 4 },
                new Users { UserName = "Abigail Jackson", UserEmail = "abigail.jackson.1347@example.com", UserPassword = "HODPass8", UniversityId = "1347", IsActive = false, DepartmentId = 8, RoleId = 4 },
                new Users { UserName = "James White", UserEmail = "james.white.1348@example.com", UserPassword = "HODPass9", UniversityId = "1348", IsActive = false, DepartmentId = 9, RoleId = 4 },
                new Users { UserName = "Sophia Harris", UserEmail = "sophia.harris.1349@example.com", UserPassword = "HODPass10", UniversityId = "1349", IsActive = false, DepartmentId = 9, RoleId = 4 },
                new Users { UserName = "Matthew Martin", UserEmail = "matthew.martin.1350@example.com", UserPassword = "HODPass11", UniversityId = "1350", IsActive = false, DepartmentId = 10, RoleId = 4 },
                new Users { UserName = "Grace Thompson", UserEmail = "grace.thompson.1351@example.com", UserPassword = "HODPass12", UniversityId = "1351", IsActive = false, DepartmentId = 10, RoleId = 4 },

                // Dean
                new Users { UserName = "Daniel Williams", UserEmail = "daniel.williams.1550@example.com", UserPassword = "DeanPass1", UniversityId = "1550", IsActive = false, DepartmentId = 5, RoleId = 5 },
                new Users { UserName = "Olivia Davis", UserEmail = "olivia.davis.1551@example.com", UserPassword = "DeanPass2", UniversityId = "1551", IsActive = false, DepartmentId = 6, RoleId = 5 },
                new Users { UserName = "Ethan Brown", UserEmail = "ethan.brown.1552@example.com", UserPassword = "DeanPass3", UniversityId = "1552", IsActive = false, DepartmentId = 7, RoleId = 5 },
                new Users { UserName = "Ava Miller", UserEmail = "ava.miller.1553@example.com", UserPassword = "DeanPass4", UniversityId = "1553", IsActive = false, DepartmentId = 8, RoleId = 5 },

                // Dean of Graduate Studies
                new Users { UserName = "Nathan Moore", UserEmail = "nathan.moore.1661@example.com", UserPassword = "DeanGradPass1", UniversityId = "1661", IsActive = false, DepartmentId = 8, RoleId = 6 },
              
                // Finance
                new Users { UserName = "Sophie Wilson", UserEmail = "sophie.wilson.1771@example.com", UserPassword = "FinancePass1", UniversityId = "1771", IsActive = false, DepartmentId = 6, RoleId = 7 }
            };

                await _context.User.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }

            if (!_context.Section.Any())
            {
                var sections = new List<Section>
            {
                //department 1: 
                new Section { Name = "Structured Programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 1, InstructorId = 33, TAId = null },  
                new Section { Name = "Structured Programming Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 1, InstructorId = 33, TAId = null },
                new Section { Name = "Structured Programming Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 1, InstructorId = 34, TAId = null },

                new Section { Name = "Structured Programming Lab Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 2, InstructorId = 34, TAId = null },
                new Section { Name = "Structured Programming Lab Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 2, InstructorId = 34, TAId = null },
                new Section { Name = "Structured Programming Lab Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 2, InstructorId = 33, TAId = null },

                new Section { Name = "Object Oriented Programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 3, InstructorId = 35, TAId = null },
                new Section { Name = "Object Oriented Programming Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 3, InstructorId = 35, TAId = null },
                new Section { Name = "Object Oriented Programming Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 3, InstructorId = 35, TAId = null },

                new Section {  Name = "Object Oriented Programming Lab Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 4, InstructorId = 35, TAId = null },
                new Section {  Name = "Object Oriented Programming Lab Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 4, InstructorId = 35, TAId = null },
                new Section {  Name = "Object Oriented Programming Lab Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 4, InstructorId = 34, TAId = null },

                new Section {  Name = "Data Structures and Introduction to Algorithms Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 5, InstructorId = 36, TAId = null },
                new Section {  Name = "Data Structures and Introduction to Algorithms Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 5, InstructorId = 36, TAId = null },
                new Section {  Name = "Data Structures and Introduction to Algorithms Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 5, InstructorId = 36, TAId = null },

                new Section {  Name = "Database Systems Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 6, InstructorId = 33, TAId = null },
                new Section {  Name = "Database Systems Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 6, InstructorId = 33, TAId = null },
                new Section {  Name = "Database Systems Section 3", StartTime = TimeSpan.Parse("13:00:00"), EndTime = TimeSpan.Parse("15:00:00"), CourseId = 6, InstructorId = 34, TAId = null },       


                //department 2: 
                new Section {  Name = "Webpage Design and Internet programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 7, InstructorId = 37, TAId = null },
                new Section {  Name = "Webpage Design and Internet programming Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 7, InstructorId = 37, TAId = null },

                new Section {  Name = "Webpage Design and Internet programming Lab Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 8, InstructorId = 37, TAId = null },
                new Section {  Name = "Webpage Design and Internet programming Lab Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 8, InstructorId = 38, TAId = null },

                new Section {  Name = "Multimedia Systems Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 9, InstructorId = 38, TAId = null },


                //department 3: 
                new Section {  Name = "Mobile Application Development Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 10, InstructorId = 39, TAId = null },
                new Section {  Name = "Advanced Topics in Programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 12, InstructorId = 40, TAId = null },
                new Section {  Name = "Server Side Programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 11, InstructorId = 39, TAId = null },


                //department 4: 
                new Section {  Name = "Computer Architecture for Machine Learning Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 13, InstructorId = 41, TAId = null },
                new Section {  Name = "Computer Architecture for Machine Learning Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 13, InstructorId = 41, TAId = null },
                new Section {  Name = "Natural Language Processing Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 14, InstructorId = 42, TAId = null },
                new Section {  Name = "Data Visualization Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 15, InstructorId = 42, TAId = null },


                //department 5
                new Section {  Name = "Database Security Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 16, InstructorId = 43, TAId = null },
                new Section {  Name = "Mobile and Wireless Security Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 17, InstructorId = 43, TAId = null },
                new Section {  Name = "Hacking Techniques and Intrusion Detection Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 18, InstructorId = 44, TAId = null },


                //department 6
                new Section {  Name = "Human Resources Management Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 19, InstructorId = 45, TAId = null },
                new Section {  Name = "Quality Management Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 20, InstructorId = 45, TAId = null },
                new Section {  Name = "Logistics Management Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 21, InstructorId = 46, TAId = null },


                //department 7
                new Section {  Name = "Governmental Accounting Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 22, InstructorId = 49, TAId = null },
                new Section {  Name = "Advanced Accounting Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 23, InstructorId = 49, TAId = null },
                new Section {  Name = "Managerial Accounting Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 24, InstructorId = 50, TAId = null },


                //department 8
                new Section {  Name = "E-Business for Business Students Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 25, InstructorId = 51, TAId = null },
                new Section {  Name = "Advanced Business Applications Programming Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 26, InstructorId = 52, TAId = null },
                new Section {  Name = "Business Intelligence Systems Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 27, InstructorId = 52, TAId = null },


                //department 9
                new Section {  Name = "E. Marketing Channels Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 28, InstructorId = 53, TAId = null },
                new Section {  Name = "Advertising Technology Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 29, InstructorId = 53, TAId = null },
                new Section {  Name = "Modern E-marketing Topics Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 30, InstructorId = null, TAId = null },

                
                //department 10 
                new Section {  Name = "Digital Logic Design Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 31, InstructorId = 55, TAId = null },
                new Section {  Name = "Digital Logic Design Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 31, InstructorId = null, TAId = null },
                new Section {  Name = "Electric Circuits (1) Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 32, InstructorId = 55, TAId = null },


                //department 11
                new Section {  Name = "Communication Principles Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 33, InstructorId = 58, TAId = null },
                new Section {  Name = "Cellular Communications Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 34, InstructorId = 58, TAId = null },
                new Section {  Name = "Introduction to IoT Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 35, InstructorId = 57, TAId = null },


                //department 12
                new Section {  Name = "Computer Architecture (1) Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 36, InstructorId = 61, TAId = null },
                new Section {  Name = "Digital Electronics Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 37, InstructorId = 59, TAId = null },
                new Section {  Name = "Digital Electronics Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 37, InstructorId = 59, TAId = null },
                new Section {  Name = "Digital Electronics Lab Section 1", StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00"), CourseId = 38, InstructorId = 60, TAId = null },
                new Section {  Name = "Digital Electronics Lab Section 2", StartTime = TimeSpan.Parse("10:15:00"), EndTime = TimeSpan.Parse("12:15:00"), CourseId = 38, InstructorId = 60, TAId = null },
            };
                await _context.Section.AddRangeAsync(sections);
                await _context.SaveChangesAsync();
            }
        }
    }
}
