using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace SweaterV1.Domain.Models
{
    public class PostModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public  string Contaiment { get; set; }
        
        //public DateTime CreatonDate { get; set; }
        //[ForeignKey("UserModel")]
        //public int? UserId { get; set; }

        //[Required]
        //public UserModel User { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime ReleaseDate { get; set; }
    }
}