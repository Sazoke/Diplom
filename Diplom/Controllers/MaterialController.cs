using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[Route("[controller]/[action]")]
public class MaterialController : Controller
{
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public MaterialController(IMaterialService materialService, IMapper mapper)
    {
        _materialService = materialService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetById([FromQuery] long id)
    {
        try
        {
            var material = _materialService.GetById(id);
            var dto = _mapper.Map<MaterialDto>(material);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public IActionResult GetByFilter([FromBody] MaterialFilter filter)
    {
        try
        {
            var materials = _materialService.GetByFilter(filter);
            if (filter.TypeId is not null)
                materials = materials.Where(m => m.TypeId == filter.TypeId);
            var dtos = materials.Select(m => _mapper.Map<FilterResultDto>(m))
                .ToList();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] MaterialEditDto materialDto)
    {
        try
        {
            await _materialService.AddOrUpdateAsync(materialDto);
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
            await _materialService.RemoveAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    public IActionResult GetTypes()
    {
        try
        {
            var types = _materialService.GetAllMaterialTypes();
            return Ok(types);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddType([FromQuery] string singleName, [FromQuery] string multipleName)
    {
        try
        {
            await  _materialService.AddMaterialType(singleName, multipleName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}