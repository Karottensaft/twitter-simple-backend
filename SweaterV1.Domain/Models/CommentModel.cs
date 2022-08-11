using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweaterV1.Domain.Models;

public class CommentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    public string CommentContainment { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = new();

    [ForeignKey("UserModel")] public int UserId { get; set; }

    public UserModel User { get; set; }

    [ForeignKey("PostModel")] public int PostId { get; set; }

    public PostModel Post { get; set; }
}