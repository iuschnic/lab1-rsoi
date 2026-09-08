namespace Api.Responses;

public class ErrorResponse
{
    public string? Message { get; init; }
}

public class ValidationErrorResponse
{
    public string? Message { get; init; }
    public Dictionary<string, string> Errors { get; init; } = new();
}