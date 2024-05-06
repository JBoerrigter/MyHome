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

    /// <summary>
    /// Gets the family for the current user
    /// </summary>
    /// <response code="200">Details for the family</response>
    /// <response code="400">Unexpected Error. See response message</response>
    /// <response code="401">Request or user is not authorized</response>
    /// <response code="404">No family was found for the user</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ActionResult<FamilyViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FamilyViewModel> GetFamily()
    {
        try
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim is null) return Unauthorized();

            var userId = Guid.Parse(claim.Value);
            if (userId == Guid.Empty) return Unauthorized();

            var family = familyService.Get(userId);

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

            var userId = Guid.Parse(claim.Value);
            if (userId == Guid.Empty) return Unauthorized();

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
