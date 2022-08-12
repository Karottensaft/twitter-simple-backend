using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweaterV1.Domain.Models;

public class PostModel
{
    public PostModel()
    {
        Comments = new List<CommentModel>();
        Likes = new List<LikeModel>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    [Required] public string? PostName { get; set; }

    [Required] public string Containment { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; } = new();

    [ForeignKey("UserModel")] public int UserId { get; set; }
    public string Username { get; set; }


    public UserModel User { get; set; }
    public List<CommentModel> Comments { get; set; }
    public List<LikeModel> Likes { get; set; }
}