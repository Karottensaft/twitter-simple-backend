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
        public string Name { get; set; }
        [Required]
        public string Contaiment { get; set; }
        [Required]
        public UserModel User { get; set; }
    }

    public class PostModelInformationDto
    {
        public string Name { get; set; }
        public string Contaiment { get; set; }
        public DateTime Created { get; set; }

        public UserModel User { get; set; }
    }

    public class PostModelChangeDto
    {
        public string Name { get; set; }
        public string Contaiment { get; set; }

    }
}
