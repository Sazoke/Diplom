using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.EducationalMaterial;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

public class EducationalMaterialController : Controller
{
    private readonly IEducationalMaterialService _educationalMaterialService;
    private readonly IMapper _mapper;

    public EducationalMaterialController(IEducationalMaterialService educationalMaterialService, IMapper mapper)
    {
        _educationalMaterialService = educationalMaterialService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        try
        {
            var material = await _educationalMaterialService.GetByIdAsync(id);
            var dto = _mapper.Map<EducationalMaterialDto>(material);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] EducationalMaterialEditDto materialDto)
    {
        try
        {
            await _educationalMaterialService.AddOrUpdateAsync(materialDto);
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
            await _educationalMaterialService.RemoveAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}