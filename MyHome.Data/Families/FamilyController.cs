using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHome.Shared;
using MyHome.Shared.Requests;
using MyHome.Shared.ViewModels;
using System.Security.Claims;

namespace MyHome.Data.Families;

[ApiController]
[Route("[Controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService familyService;

    public FamilyController(IFamilyService familyService)
    {
        this.familyService = familyService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<FamilyViewModel> GetFamily(int id)
    {
        try
        {
            var family = familyService.Get(id);

            if (family is null)
            {
                return NotFound();
            }

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

    [HttpPost("Create")]
    public ActionResult<FamilyViewModel> CreateFamily(FamilyCreateRequest request)
    {
        try
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim is null) return Unauthorized();

            var userId = claim.Value;
            if (userId is null) return Unauthorized();

            var familyId = familyService.Create(request.Name, userId);
            var response = new FamilyViewModel
            {
                Id = familyId,
                Name = request.Name
            };

            return Created(Request.PathBase, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Join/{id}")]
    public ActionResult<FamilyViewModel> JoinFamily(string id)
    {
        return BadRequest("Not implemented yet");
    }

}
