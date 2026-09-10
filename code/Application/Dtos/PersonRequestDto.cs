namespace Application.Dtos;

public class PersonRequestDto
{
    public required string Name { get; init; }
    public int? Age { get; init; }
    public string? Address { get; init; }
    public string? Work { get; init; }
}