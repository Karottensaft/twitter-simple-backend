using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Domain.Models
{
    public class CommentModelInformationDto
    {
        public string CommentContainment { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
    public class CommentModelCreationDto
    {
        public string CommentContainment { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
    public class CommentModelChangeDto
    {
        public string CommentContainment { get; set; }
    }
}
