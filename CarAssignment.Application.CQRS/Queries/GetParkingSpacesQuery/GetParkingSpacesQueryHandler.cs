using CarAssignment.Application.CQRS.Models;
using CarAssignment.Core.Abstractions;
using CarAssignment.Infrastructure;
using MediatR;

namespace CarAssignment.Application.CQRS.Queries.GetParkingSpacesQuery;

public class GetParkingSpacesQueryHandler(IParkingSlotRepository parkingSlotRepository) : IRequestHandler<GetParkingSpacesQuery, GetParkingSpacesQueryResponse>
{
    public async Task<GetParkingSpacesQueryResponse> Handle(GetParkingSpacesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new GetParkingSpacesQueryResponse(parkingSlotRepository.GetAvailableCapacity(),
            parkingSlotRepository.GetOccupiedCapacity()));
    }
}