namespace CarAssignment.Core.Exceptions;

public class NotAvailableSpaceException : Exception
{
    public override string Message => "No available space.";
}