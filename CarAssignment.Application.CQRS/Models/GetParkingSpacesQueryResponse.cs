namespace CarAssignment.Application.CQRS.Models;

public record GetParkingSpacesQueryResponse(int AvailableSpaces, int OccupiedSpaces);