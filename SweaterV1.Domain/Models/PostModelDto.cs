using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Domain.Models
{
    //public class PostMoelInformtionDto
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //}

    public class PostModelCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contaiment { get; set; }
    }
}
