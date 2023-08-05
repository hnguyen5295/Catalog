using System.ComponentModel.DataAnnotations;
using Catalog.Entities;

namespace Catalog.Dtos
{
  public record AuthRequestDto
  {
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
  }

  public class AuthResponseDto
  {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string Token { get; set; }


    public AuthResponseDto(User user, string token)
    {
      Id = user.Id;
      FirstName = user.FirstName;
      LastName = user.LastName;
      Username = user.Username;
      Token = token;
    }
  }
}