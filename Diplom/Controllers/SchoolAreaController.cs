using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SchoolAreaController : Controller
{
    private readonly ISchoolAreaService _schoolAreaService;
    private readonly IMapper _mapper;

    public SchoolAreaController(ISchoolAreaService schoolAreaService, IMapper mapper)
    {
        _schoolAreaService = schoolAreaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        try
        {
            var result = await _schoolAreaService.GetByIdAsync(id);
            var dto = _mapper.Map<SchoolAreaDto>(result);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _schoolAreaService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] SchoolAreaDto schoolAreaDto)
    {
        try
        {
            await _schoolAreaService.AddOrUpdate(schoolAreaDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}