using System;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Infrastructure.Dtos.User;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
    {
        try
        {
            var newId = await _userService.CreateUserAsync(userDto);
            return Ok(newId);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile([FromQuery] string id)
    {
        if (id is null && !User.IsAuthenticated())
            return NotFound();
        try
        {
            var dto = await _userService.GetProfile(id);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditDescription([FromBody] string description)
    {
        try
        {
            await _userService.EditDescription(description);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditImage([FromBody] IFormFile image)
    {
        try
        {
            var fileName = await _userService.EditImage(image);
            return Ok(fileName);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}