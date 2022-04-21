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
    public IActionResult GetById([FromQuery] long id)
    {
        try
        {
            var material = _educationalMaterialService.GetById(id);
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
    public IActionResult AddOrUpdate([FromBody] EducationalMaterialEditDto materialDto)
    {
        try
        {
            _educationalMaterialService.AddOrUpdateAsync(materialDto);
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