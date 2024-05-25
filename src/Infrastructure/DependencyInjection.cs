using TAManagment.Application.Common.Interfaces;
using TAManagment.Domain.Constants;
using TAManagment.Infrastructure.Data;
using TAManagment.Infrastructure.Data.Interceptors;
using TAManagment.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;
using TAManagment.Domain.Entities.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IWorkRecordService, WorkRecordService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IHODService, HODService>();
        services.AddScoped<IInstructorService, InstructorService>();
        services.AddScoped<ITAService, TAService>();
        services.AddScoped<IPhdTAService, PhdTAService>();
        services.AddScoped<IDeanService, DeanService>();
        services.AddScoped<IDeanOfGraduateStudiesService, DeanOfGraduateStudiesService>();
        services.AddScoped<IFinanceService, FinanceService>();


        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);


        return services;
    }
}
