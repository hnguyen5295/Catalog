using Catalog.Entities;
using Catalog.Dtos;
using Catalog.Auth;

namespace Catalog.Repositories
{
  public interface IUsersRepository
  {
    AuthResponseDto? Authenticate(AuthRequestDto authRequestDto);
    IEnumerable<User> GetAll();
    User? GetById(int id);
  }

  public class UsersRepository : IUsersRepository
  {

    private readonly IJwtUtils _jwtUtils;

    public UsersRepository(IJwtUtils jwtUtils)
    {
      _jwtUtils = jwtUtils;
    }

    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
    };

    public AuthResponseDto? Authenticate(AuthRequestDto authRequestDto)
    {
      var user = _users.SingleOrDefault(x => x.Username == authRequestDto.Username && x.Password == authRequestDto.Password);

      // return null if user not found
      if (user == null) return null;

      // authentication successful so generate jwt token
      var token = _jwtUtils.GenerateJwtToken(user);

      return new AuthResponseDto(user, token);
    }

    public IEnumerable<User> GetAll()
    {
      return _users;
    }

    public User? GetById(int id)
    {
      return _users.FirstOrDefault(x => x.Id == id);
    }
  }
}