using Catalog.Repositories;

namespace Catalog.Auth
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, IUsersRepository usersRepository, IJwtUtils jwtUtils)
    {
      var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
      var userId = jwtUtils.ValidateJwtToken(token);
      if (userId != null)
      {
        // attach user to context on successful jwt validation
        context.Items["User"] = usersRepository.GetById(userId.Value);
      }

      await _next(context);
    }

  }
}