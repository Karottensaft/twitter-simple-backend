using System.ComponentModel.DataAnnotations;


namespace SweaterV1.Domain.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }

        public int UserId { get; set; }
        public string? Text { get; set; }

        public DateTime Date = DateTime.Now;
    }
}