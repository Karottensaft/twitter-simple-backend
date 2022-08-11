using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace SweaterV1.Domain.Models
{
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string? PostName { get; set; }
        public string Containment { get; set; }

        public DateTime CreationDate { get; set; }
        [ForeignKey("UserModel")]
        public int UserId { get; set; }

        
        public UserModel User { get; set; }
        public List<CommentModel> Comments { get; set; }
        public  List<LikeModel> Likes { get; set; }
        public PostModel()
        {
            Comments = new List<CommentModel>();
            Likes = new List<LikeModel>();
        }

    }
}