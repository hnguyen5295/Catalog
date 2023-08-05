using Catalog.Auth;
using Catalog.Dtos;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{

  [ApiController]
  [Authorize]
  [Route("users")]
  public class UsersController : ControllerBase
  {

    private readonly IUsersRepository _usersRepository;
    public UsersController(IUsersRepository usersRepository)
    {
      _usersRepository = usersRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Authenticate(AuthRequestDto authRequestDto)
    {
      var response = _usersRepository.Authenticate(authRequestDto);
      if (response == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var users = _usersRepository.GetAll();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _usersRepository.GetById(id);
      return Ok(user);
    }
  }
}