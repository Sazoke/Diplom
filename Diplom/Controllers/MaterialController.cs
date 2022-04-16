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
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        try
        {
            var material = await _materialService.GetByIdAsync(id);
            var dto = _mapper.Map<MaterialDto>(material);
            return Ok(dto);
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
            var materials = await _materialService.GetByFilterAsync(filter);
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
}