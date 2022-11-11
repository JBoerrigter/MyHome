using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;
using MyHome.Shared.Requests;

namespace MyHome.Data.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService homeService;
        private readonly Shared.IUserService<ApplicationUser> userService;

        public HomeController(IHomeService homeService, Shared.IUserService<ApplicationUser> userService)
        {
            this.homeService = homeService;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<HomeViewModel> GetHome(int id)
        {
            try
            {
                HomeViewModel home = homeService.Get(id);
                if (home is null) return NotFound();
                return Ok(home);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<HomeCreateResponse> CreateHome(HomeCreateRequest request)
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claim is null) return Unauthorized();

                var userId = claim.Value;
                var familyId = userService.GetFamilyId(userId);
                if (familyId is null) return BadRequest("You need to create a family first");

                var id = homeService.Create(
                    familyId.Value,
                    request.Street,
                    request.Number,
                    request.PostalCode,
                    request.City);

                HomeCreateResponse response = new HomeCreateResponse();
                response.Id = id;

                return Created(Request.PathBase, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}