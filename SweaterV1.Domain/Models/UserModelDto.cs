using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SweaterV1.Domain.Models
{
    public class UserModelLoginDto
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class UserModelRegistrationDto
    {
        [ConcurrencyCheck]
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password id required.")]
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
        public string Username { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PostModel> Posts { get; set; }

    }

    public class UserModelChangeDto
    {
        public string Password { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserModelAutentificationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
