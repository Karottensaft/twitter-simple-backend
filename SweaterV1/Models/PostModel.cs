using System.ComponentModel.DataAnnotations;
namespace SweaterV1.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
    }
}