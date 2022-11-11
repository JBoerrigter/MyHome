using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;
using MyHome.Shared.Requests;
using MyHome.Shared.ViewModels;

namespace MyHome.Data.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService familyService;

        public FamilyController(IFamilyService familyService)
        {
            this.familyService = familyService;
        }

        [HttpGet("{id}")]
        public ActionResult<FamilyViewModel> GetFamily(int id)
        {
            try
            {
                var family = familyService.Get(id);
                var viewModel = new FamilyViewModel
                {
                    Id = family.Id,
                    Name = family.Name
                };
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<FamilyCreateResponse> CreateFamily(FamilyCreateRequest request)
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claim is null) return Unauthorized();

                var userId = claim.Value;
                if (userId is null) return Unauthorized();

                var familyId = familyService.Create(request.Name, userId);
                var response = new FamilyCreateResponse
                {
                    Id = familyId
                };

                return Created(Request.PathBase, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public ActionResult<FamilyCreateResponse> JoinFamily(string id)
        {
            return BadRequest("Not implemented yet");
        }

    }
}

