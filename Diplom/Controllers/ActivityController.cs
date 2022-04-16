using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Create()
    {
        try
        {
            var result = await _activityService.CreateAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditName([FromQuery] long id, [FromBody] string name)
    {
        try
        {
            await _activityService.EditNameAsync(id, name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditType([FromQuery] long id, [FromBody] DateTime date)
    {
        try
        {
            await _activityService.EditDate(id, date);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditSchoolArea([FromQuery] long id, [FromBody] long areaId)
    {
        try
        {
            await _activityService.EditSchoolAreaAsync(id, areaId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditImage([FromQuery] long id, [FromBody] string file)
    {
        try
        {
            await _activityService.EditImageAsync(id, file);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditTags([FromQuery] long id, [FromBody] List<long> tags)
    {
        try
        {
            await _activityService.EditTagsAsync(id, tags);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditDescription([FromQuery] long id, [FromBody] string description)
    {
        try
        {
            await _activityService.EditDescriptionAsync(id, description);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}