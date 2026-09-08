using System.ComponentModel.DataAnnotations;

namespace Api.Requests;

public class PersonRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; } = null!;
    public int? Age { get; init; }
    public string? Address { get; init; }
    public string? Work { get; init; }
}