namespace CarAssignment.Core.Exceptions;

public class ConflictException(string message) : Exception
{
    public override string Message => message;
}