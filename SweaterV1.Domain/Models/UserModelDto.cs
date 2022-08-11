using System.ComponentModel.DataAnnotations;

namespace SweaterV1.Domain.Models;

public class UserModelLoginDto
{
    public int UserId { get; set; }

    [Required] public string Username { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}

public class UserModelRegistrationDto
{
    [ConcurrencyCheck] [Required] public string Username { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Mail { get; set; } = string.Empty;

    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;
}

public class UserModelInformationDto
{
    public string Username { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<PostModel> Posts { get; set; } = new();
}

public class UserModelChangeDto
{
    public string Password { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class UserModelAuthDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}