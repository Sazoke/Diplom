using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    private readonly IMapper _mapper;
    
    public ActivityController(IActivityService activityService, IMapper mapper)
    {
        _activityService = activityService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetById([FromQuery] long id)
    {
        try
        {
            var activity = _activityService.GetById(id);
            var dto = _mapper.Map<ActivityDto>(activity);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public IActionResult GetByFilter([FromBody] Filter filter)
    {
        try
        {
            var activities = _activityService.GetByFilter(filter);
            var dtos = activities.Select(a => _mapper.Map<FilterResultDto>(a)).ToList();
            return Ok(dtos);
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