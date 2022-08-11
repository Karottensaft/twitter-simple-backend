using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SweaterV1.Domain.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } = "user";

        public List<PostModel> Posts { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<LikeModel> Likes { get; set; }

        public UserModel()
        {
            Posts = new List<PostModel>();
            Comments = new List<CommentModel>();
            Likes = new List<LikeModel>();
        }
    }
}