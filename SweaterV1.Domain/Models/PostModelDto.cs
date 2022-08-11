using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Domain.Models
{
    public class PostModelCreationDto
    {
        [Required]
        public string PostName { get; set; }

        [Required]
        public string Containment { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class PostModelInformationDto
    {
        public string PostName { get; set; }
        public string Containment { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }

    public class PostModelChangeDto
    {
        public string PostName { get; set; }
        public string Containment { get; set; }

    }
}
