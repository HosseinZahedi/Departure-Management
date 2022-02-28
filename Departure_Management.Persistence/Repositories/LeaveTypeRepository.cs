using Departure_Management.Application.Contracts.Persistence;
using Departure_Management.Domain;

namespace Departure_Management.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly DepartureManagementDbContext _dbContext;

    public LeaveTypeRepository(DepartureManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}