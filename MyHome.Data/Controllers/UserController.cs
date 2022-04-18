﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MyHome.Shared;

namespace MyHome.Data.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService<ApplicationUser> userService;
        private readonly ITokenCreator<ApplicationUser> tokenCreator;

        public UserController(IUserService<ApplicationUser> userService, ITokenCreator<ApplicationUser> tokenCreator)
        {
            this.userService = userService;
            this.tokenCreator = tokenCreator;
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public ActionResult<UserViewModel> Create(UserViewModel model)
        {
            UserViewModel user = userService.Create(model.Username, model.Email, model.Password, model.PasswordConfirmation);

            if (user is null)
            {
                return BadRequest("Benutzer konnte nicht erstellt werden");
            }
            else
            {
                return Ok(user);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid) return Unauthorized();

            ApplicationUser user = userService.Authenticate(model.Username, model.Password);
            if (user is null) return Unauthorized();

            string token = tokenCreator.CreateToken(user);
            if (string.IsNullOrWhiteSpace(token)) return Unauthorized();

            return Ok(token);
        }
    }
}
