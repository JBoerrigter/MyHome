using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;
using MyHome.Shared.Requests;

namespace MyHome.Data.Homes
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

        [HttpGet]
        public ActionResult<IEnumerable<HomeViewModel>> GetHomes()
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                if (claim is null) return Unauthorized();

                var familyId = Guid.Parse(claim.Value);
                if (familyId == Guid.Empty) return Unauthorized();

                IEnumerable<HomeViewModel> homes = homeService.GetByFamilyId(familyId);
                if (homes == Enumerable.Empty<HomeViewModel>()) return NotFound();
                return Ok(homes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult<HomeViewModel> CreateHome(HomeCreateRequest request)
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claim is null) return Unauthorized();

                var userId = Guid.Parse(claim.Value);
                var familyId = request.FamilyId;
                if (familyId == Guid.Empty) return BadRequest("You need to create a family first");

                var id = homeService.Create(
                    request.FamilyId,
                    request.Street,
                    request.Number,
                    request.PostalCode,
                    request.City);

                HomeViewModel response = new HomeViewModel();
                response.Id = id;
                response.Street = request.Street;
                response.Number = request.Number;
                response.PostalCode = request.PostalCode;
                response.City = request.City;

                return Created(Request.PathBase, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}