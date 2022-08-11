using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Domain.Models
{
    public class LikeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("PostModel")]
        public int? PostId { get; set; }
        public PostModel Post { get; set; }
    }
}
