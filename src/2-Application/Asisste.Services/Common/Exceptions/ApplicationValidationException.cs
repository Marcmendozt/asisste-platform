namespace Asisste.Services.Common.Exceptions;

public sealed class ApplicationValidationException : Exception
{
    public ApplicationValidationException(string message)
        : base(message)
    {
    }
}