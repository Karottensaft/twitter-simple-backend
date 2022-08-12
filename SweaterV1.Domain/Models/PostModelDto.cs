using System.ComponentModel.DataAnnotations;

namespace SweaterV1.Domain.Models;

public class PostModelCreationDto
{
    [Required] public string PostName { get; set; } = string.Empty;

    [Required] public string Containment { get; set; } = string.Empty;
}

public class PostModelInformationDto
{
    public string PostName { get; set; } = string.Empty;
    public string Containment { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
}

public class PostModelChangeDto
{
    public string PostName { get; set; } = string.Empty;
    public string Containment { get; set; } = string.Empty;
}