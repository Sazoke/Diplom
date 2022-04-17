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
using Microsoft.AspNetCore.Authorization;
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
            if (id == _applicationContext.CurrentUserId)
                return await GetCurrentUserProfile();

            var user = await _userService.GetProfile(id);
            var dto = _mapper.Map<UserProfileDto>(user);
            dto.Activities = user.Activities.Take(4)
                .Select(_mapper.Map<ActivityProfilePreview>)
                .ToList();
            dto.Materials = user.Materials.Take(4)
                .Select(_mapper.Map<MaterialProfilePreview>)
                .ToList();
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserProfile()
    {
        try
        {
            var user = await _userService.GetProfile(_applicationContext.CurrentUserId);
            var dto = _mapper.Map<UserProfileDto>(user);
            dto.Activities = user.Activities.Take(3)
                .Select(_mapper.Map<ActivityProfilePreview>)
                .ToList();
            dto.Materials = user.Materials.Take(3)
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
    [Authorize]
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