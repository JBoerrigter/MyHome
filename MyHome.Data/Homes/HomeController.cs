﻿using System;
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

        [HttpPost("Create")]
        public ActionResult<HomeViewModel> CreateHome(HomeCreateRequest request)
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claim is null) return Unauthorized();

                var userId = claim.Value;
                var familyId = request.FamilyId;
                if (familyId <= 0) return BadRequest("You need to create a family first");

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