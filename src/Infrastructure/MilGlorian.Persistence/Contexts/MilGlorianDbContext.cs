using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilGlorian.Domain.Entities;
using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Persistence.Contexts;

public class MilGlorianDbContext : IdentityDbContext
{
    public MilGlorianDbContext(DbContextOptions<MilGlorianDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Biography> Biographies { get; set; }
    public virtual DbSet<Branch> Branches { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<CompanyDetail> CompanyDetails { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Industry> Industries { get; set; }
    public virtual DbSet<Vacancy> Vacancies { get; set; }
    public virtual DbSet<VacancyDetail> VacancyDetails { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var data in ChangeTracker.Entries<BaseEntity>())
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.ModifiedDate = DateTime.UtcNow,
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
