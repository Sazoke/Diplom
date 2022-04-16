using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.Tag;
using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TagController : Controller
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagController(ITagService tagService, IMapper mapper)
    {
        _tagService = tagService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        var result = await _tagService.GetByIdAsync(id);
        if (result is null)
            return NotFound();
        var dto = _mapper.Map<TagDto>(result);
        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetBySchoolArea([FromQuery] long schoolAreaId)
    {
        try
        {
            var result = _tagService.GetBySchoolArea(schoolAreaId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var result = await _tagService.CreateAsync();
        var dto = _mapper.Map<TagDto>(result);
        return Ok(dto);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditName([FromBody] TagDto tagDto)
    {
        if (tagDto.Id is null || tagDto.Name is null)
            return BadRequest("Id is null");
        var result = await _tagService.TryEditNameAsync(tagDto.Id.Value, tagDto.Name);
        return result ? Ok() : BadRequest();
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> EditSchoolArea([FromBody] TagDto tagDto)
    {
        if (tagDto.Id is null || tagDto.SchoolAreaId is null)
            return BadRequest("Id is null");
        var result = await _tagService.TryEditSchoolAreaAsync(tagDto.Id.Value, tagDto.SchoolAreaId.Value);
        return result ? Ok() : BadRequest();
    }
}