using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Domain.Models
{
    public class LikeModelInformationDto
    {
        public DateTime CreatonDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
    public class LikeModelCreationDto
    {
        public DateTime CreatonDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
