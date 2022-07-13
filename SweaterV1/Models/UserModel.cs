using System.ComponentModel.DataAnnotations;
namespace SweaterV1.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Mail { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }

}