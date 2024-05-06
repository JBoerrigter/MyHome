using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;
using MyHome.Shared.Requests;

namespace MyHome.Data.Users;

[Authorize]
[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService<ApplicationUser> userService;

    public UserController(IUserService<ApplicationUser> userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public ActionResult<UserViewModel> Create(UserCreateRequest request)
    {
        UserViewModel user = userService.Create(request.UserName, request.Email, request.Password, request.PasswordConfirmation);

        if (user is null)
        {
            return BadRequest("Benutzer konnte nicht erstellt werden");
        }
        else
        {
            return Created(Request.PathBase, user);
        }
    }
}