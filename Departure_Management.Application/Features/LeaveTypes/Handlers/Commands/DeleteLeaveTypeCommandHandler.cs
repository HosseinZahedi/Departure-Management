using AutoMapper;
using Departure_Management.Application.Contracts.Persistence;
using Departure_Management.Application.Exceptions;
using Departure_Management.Application.Features.LeaveTypes.Requests.Commands;
using Departure_Management.Domain;
using MediatR;

namespace Departure_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.Id);

        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        await _unitOfWork.LeaveTypeRepository.Delete(leaveType);
        await _unitOfWork.Save();

        return Unit.Value;
    }
}