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
        public string CommentText { get; set; }
        public DateTime CreatonDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
    public class CommentModelCreationDto
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
    public class CommentModelChangeDto
    {
        public string CommentText { get; set; }
    }
}
