namespace Application.Dtos;

public class PersonDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int? Age { get; init; }
    public string? Address { get; init; }
    public string? Work { get; init; }
}