using CarAssignment.Application.CQRS.Models;
using CarAssignment.Core.Abstractions;
using MediatR;

namespace CarAssignment.Application.CQRS.Queries.GetParkingSpacesQuery;

public class GetParkingSpacesQueryHandler(IParkingService parkingService) : IRequestHandler<GetParkingSpacesQuery, GetParkingSpacesQueryResponse>
{
    public async Task<GetParkingSpacesQueryResponse> Handle(GetParkingSpacesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new GetParkingSpacesQueryResponse(parkingService.GetAvailableCapacity(),
            parkingService.GetOccupiedCapacity()));
    }
}