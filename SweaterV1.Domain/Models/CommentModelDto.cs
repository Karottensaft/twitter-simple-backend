using System.ComponentModel.DataAnnotations;

namespace SweaterV1.Domain.Models;

public class CommentModelInformationDto
{
    public string CommentContainment { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class CommentModelCreationDto
{
    [Required] public string CommentContainment { get; set; } = string.Empty;

    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class CommentModelChangeDto
{
    [Required] public string CommentContainment { get; set; } = string.Empty;
}