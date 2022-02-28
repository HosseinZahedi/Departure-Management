using Departure_Management.Application.Constants;
using Departure_Management.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;

namespace Departure_Management.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly DepartureManagementDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ILeaveTypeRepository _leaveTypeRepository;
    private ILeaveRequestRepository _leaveRequestRepository;


    public UnitOfWork(DepartureManagementDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public ILeaveTypeRepository LeaveTypeRepository => 
        _leaveTypeRepository ??= new LeaveTypeRepository(_context);
    public ILeaveRequestRepository LeaveRequestRepository => 
        _leaveRequestRepository ??= new LeaveRequestRepository(_context);
        
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task Save() 
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

        await _context.SaveChangesAsync(username);
    }
}