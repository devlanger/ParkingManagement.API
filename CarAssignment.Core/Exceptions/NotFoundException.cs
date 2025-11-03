namespace CarAssignment.Core.Exceptions;

public class NotFoundException(string message) : Exception
{
    public override string Message => message;
}