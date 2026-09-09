namespace Storage.Models;

public class PersonDb
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Address { get; set; }
    public string? Work { get; set; }
}