using Departure_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace Departure_Management.Persistence;

public class DepartureManagementDbContext : AuditableDbContext
{
    public DepartureManagementDbContext(DbContextOptions<DepartureManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartureManagementDbContext).Assembly);
    }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
}