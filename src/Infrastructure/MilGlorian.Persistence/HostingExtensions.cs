using Microsoft.Extensions.DependencyInjection;
using MilGlorian.Application.Abstract.Repositories.Biographies;
using MilGlorian.Application.Abstract.Repositories.Branches;
using MilGlorian.Application.Abstract.Repositories.Categories;
using MilGlorian.Application.Abstract.Repositories.Cities;
using MilGlorian.Application.Abstract.Repositories.Companies;
using MilGlorian.Application.Abstract.Repositories.CompanyDetails;
using MilGlorian.Application.Abstract.Repositories.Departments;
using MilGlorian.Application.Abstract.Repositories.Industries;
using MilGlorian.Application.Abstract.Repositories.Vacancies;
using MilGlorian.Application.Abstract.Repositories.VacancyDetails;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Persistence.Concrete.Repositories.Biographies;
using MilGlorian.Persistence.Concrete.Repositories.Branches;
using MilGlorian.Persistence.Concrete.Repositories.Categories;
using MilGlorian.Persistence.Concrete.Repositories.Cities;
using MilGlorian.Persistence.Concrete.Repositories.Companies;
using MilGlorian.Persistence.Concrete.Repositories.CompanyDetails;
using MilGlorian.Persistence.Concrete.Repositories.Departments;
using MilGlorian.Persistence.Concrete.Repositories.Industries;
using MilGlorian.Persistence.Concrete.Repositories.Vacancies;
using MilGlorian.Persistence.Concrete.Repositories.VacancyDetails;
using MilGlorian.Persistence.Concrete.Services;

namespace MilGlorian.Persistence;

public static class HostingExtensions
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IBiographyReadRepository,BiographyReadRepository>();
        services.AddScoped<IBiographyWriteRepository,BiographyWriteRepository>();
        services.AddScoped<IBranchReadRepository,BranchReadRepository>();
        services.AddScoped<IBranchWriteRepository,BranchWriteRepository>();
        services.AddScoped<ICategoryReadRepository,CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository,CategoryWriteRepository>();
        services.AddScoped<ICityReadRepository,CityReadRepository>();
        services.AddScoped<ICityWriteRepository,CityWriteRepository>();
        services.AddScoped<ICompanyReadRepository,CompanyReadRepository>();
        services.AddScoped<ICompanyWriteRepository,CompanyWriteRepository>();
        services.AddScoped<ICompanyDetailReadRepository,CompanyDetailReadRepository>();
        services.AddScoped<ICompanyDetailWriteRepository,CompanyDetailWriteRepository>();
        services.AddScoped<IDepartmentReadRepository,DepartmentReadRepository>();
        services.AddScoped<IDepartmentWriteRepository,DepartmentWriteRepository>();
        services.AddScoped<IIndustryReadRepository,IndustryReadRepository>();
        services.AddScoped<IIndustryWriteRepository,IndustryWriteRepository>();
        services.AddScoped<IVacancyReadRepository,VacancyReadRepository>();
        services.AddScoped<IVacancyWriteRepository,VacancyWriteRepository>();
        services.AddScoped<IVacancyDetailReadRepository,VacancyDetailReadRepository>();
        services.AddScoped<IVacancyDetailWriteRepository, VacancyDetailWriteRepository>();

        services.AddScoped<ICityService,CityService>();
    }
}
