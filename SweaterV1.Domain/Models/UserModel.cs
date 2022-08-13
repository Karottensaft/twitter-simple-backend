using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweaterV1.Domain.Models;

public class UserModel
{
    public UserModel()
    {
        Posts = new List<PostModel>();
        Comments = new List<CommentModel>();
        Likes = new List<LikeModel>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string Role { get; set; } = "user";

    public List<PostModel> Posts { get; set; }
    public List<CommentModel> Comments { get; set; }
    public List<LikeModel> Likes { get; set; }
}