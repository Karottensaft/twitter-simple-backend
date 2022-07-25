using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SweaterV1.Domain.Models
{
    public class UserModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        //[Required]
        public string Login { get; set; }
        //[Required]
        public string Password { get; set; }
        public string Mail { get; set; }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }

        public string Role { get; set; }

        //public List<PostModel> Posts { get; set; }

        //public UserModel()
        //{
        //    Posts = new List<PostModel>();
        //}
    }
}