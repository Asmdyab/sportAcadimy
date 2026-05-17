namespace SportAcademy.Application.DTOs.BranchDtos;

public class BranchCardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? CoX { get; set; }
    public string? CoY { get; set; }
}
