using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SweaterV1.Domain.Models
{
    public class UserModelLoginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class UserModelRegistrationDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }

    public class UserModelInformationDto
    {
        public string Login { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserModelChangeDto
    {
        public string Password { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
