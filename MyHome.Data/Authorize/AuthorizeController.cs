using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;

namespace MyHome.Data.Authorize;

[ApiController]
[Route("[Controller]")]
public class AuthorizeController : ControllerBase
{
    IUserService<ApplicationUser> userService;
    ITokenCreator<ApplicationUser> tokenCreator;

    public AuthorizeController(
        IUserService<ApplicationUser> userService,
        ITokenCreator<ApplicationUser> tokenCreator)
    {
        this.userService = userService;
        this.tokenCreator = tokenCreator;
    }

    [HttpPost]
    public async Task<IActionResult> Authorize(AuthorizeRequest request)
    {
        if (!ModelState.IsValid) return Unauthorized();

        ApplicationUser user = await userService.AuthenticateAsync(request.UserName, request.Password);
        if (user is null) return Unauthorized();

        string token = tokenCreator.CreateToken(user);
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();

        return Ok(token);
    }
}