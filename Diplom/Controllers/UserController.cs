using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Duende.IdentityServer.Extensions;
using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Material;
using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _applicationContext;

    public UserController(IUserService userService, IMapper mapper, ApplicationContext applicationContext)
    {
        _userService = userService;
        _mapper = mapper;
        _applicationContext = applicationContext;
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
            var countOfComponents = id is null ? 3 : 4;
            id ??= _applicationContext.CurrentUserId;

            var user = await _userService.GetProfile(id);
            var dto = _mapper.Map<UserProfileDto>(user);
            dto.Activities = user.Activities.Take(countOfComponents)
                .Select(_mapper.Map<ActivityProfilePreview>)
                .ToList();
            dto.Materials = user.Materials.Take(countOfComponents)
                .Select(_mapper.Map<MaterialProfilePreview>)
                .ToList();
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileEditDto profileEditDto)
    {
        try
        {
            await _userService.UpdateProfile(profileEditDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}