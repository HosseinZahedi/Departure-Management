namespace Departure_Management.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    ILeaveRequestRepository LeaveRequestRepository { get; }
    ILeaveTypeRepository LeaveTypeRepository { get; }
    Task Save();
}