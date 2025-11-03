using CarAssignment.Application.CQRS.Models;
using MediatR;

namespace CarAssignment.Application.CQRS.Queries.GetParkingSpacesQuery;

public record GetParkingSpacesQuery : IRequest<GetParkingSpacesQueryResponse>;