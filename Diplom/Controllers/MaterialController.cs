using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[Route("[controller]/[action]")]
public class MaterialController : Controller
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        try
        {
            var result = await _materialService.GetByIdAsync(id);
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
            var result = await _materialService.GetByFilterAsync(filter);
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
            var result = await _materialService.CreateAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditName([FromBody] long id, [FromBody] string name)
    {
        try
        {
            await _materialService.EditNameAsync(id, name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditType([FromBody] long id, [FromBody] string type)
    {
        try
        {
            await _materialService.EditTypeAsync(id, type);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditSchoolArea([FromBody] long id, [FromBody] long areaId)
    {
        try
        {
            await _materialService.EditSchoolAreaAsync(id, areaId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditImage([FromBody] long id, [FromBody] string file)
    {
        try
        {
            await _materialService.EditImageAsync(id, file);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditTags([FromBody] long id, [FromBody] List<long> tags)
    {
        try
        {
            await _materialService.EditTagsAsync(id, tags);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditDescription([FromBody] long id, [FromBody] string description)
    {
        try
        {
            await _materialService.EditDescriptionAsync(id, description);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddFragment([FromBody] long id)
    {
        try
        {
            var result = await _materialService.AddFragmentAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditFragment([FromBody] FragmentDto fragmentDto)
    {
        try
        {
            await _materialService.EditFragmentAsync(fragmentDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> RemoveFragment([FromBody] long fragmentId)
    {
        try
        {
            await _materialService.RemoveFragmentAsync(fragmentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}