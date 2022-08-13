namespace SweaterV1.Domain.Models;

public class LikeModelInformationDto
{
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class LikeModelCreationDto
{
    public DateTime CreationDate { get; set; }
}