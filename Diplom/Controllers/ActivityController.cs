using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.Activity;
using Infrastructure.Models;
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
    public async Task<IActionResult> GetActivities([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var activities = await _activityService.GetActivitiesPagedList(page, pageSize);
        var dtos = activities.Select(a => _mapper.Map<Activity>(a)).ToList();
        return Ok(dtos);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(long id)
    {
        var activity = await _activityService.GetById(id);
        var dto = _mapper.Map<ActivityDto>(activity);
        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateActivity([FromBody] ActivityDto dto)
    {
        var activity = _mapper.Map<Activity>(dto);
        await _activityService.Add(activity);
        return Ok(activity);
    }
}