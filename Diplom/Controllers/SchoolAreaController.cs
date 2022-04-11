using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Services.Interfaces;
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
    public async Task<IActionResult> GetById([FromBody] long id)
    {
        var result = await _schoolAreaService.GetByIdAsync(id);
        if (result is null)
            return NotFound();
        var dto = _mapper.Map<SchoolAreaDto>(result);
        return Ok(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _schoolAreaService.GetAllAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var result = await _schoolAreaService.CreateAsync();
        var dto = _mapper.Map<SchoolAreaDto>(result);
        return Ok(dto);
    }
    
    [HttpPut]
    public async Task<IActionResult> EditName([FromBody] SchoolAreaDto areaDto)
    {
        if (areaDto.Id is null || areaDto.Name is null)
            return BadRequest("Id is null");
        var result = await _schoolAreaService.TryEditNameAsync(areaDto.Id.Value, areaDto.Name);
        return result ? Ok() : BadRequest();
    }
}