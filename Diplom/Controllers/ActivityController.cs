using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Base;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ActivityController : Controller
{
    private readonly IActivityService _activityService;
    
    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        try
        {
            var result = await _activityService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetByFilter([FromBody] FilterDto filter)
    {
        try
        {
            var result = await _activityService.GetByFilterAsync(filter);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] ActivityEditDto activityEditDto)
    {
        try
        {
            await _activityService.AddOrUpdateAsync(activityEditDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Remove([FromQuery] long id)
    {
        try
        {
            await _activityService.RemoveAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}