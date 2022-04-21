using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Duende.IdentityServer.Extensions;
using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Base;
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
        if (id is null)
            return NotFound();
        try
        {
            var dto = await GetProfileDto(4);
            if (dto is null)
                return NotFound();
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
        if (!User.IsAuthenticated())
            return BadRequest();
        try
        {
            var dto = await GetProfileDto(3);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    private async Task<UserProfileDto> GetProfileDto(int countOfMaterials)
    {
        var user = await _userService.GetProfile(_applicationContext.CurrentUserId);
        if (user is null)
            return null;
        var dto = _mapper.Map<UserProfileDto>(user);
        dto.Activities = user.Activities.Take(countOfMaterials)
            .Select(_mapper.Map<ActivityProfilePreview>)
            .ToList();
        dto.Materials = user.Materials.Take(countOfMaterials)
            .Select(_mapper.Map<MaterialProfilePreview>)
            .ToList();
        return dto;
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

    [HttpPost]
    public async Task<IActionResult> GetByFilter(Filter filter)
    {
        try
        {
            var users = _userService.GetByFilter(filter);
            var dtos = users.Select(_mapper.Map<ProfileFilterPreview>).ToList();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}