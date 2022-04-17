using System;
using System.Collections.Generic;
using System.Linq;
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
    public async Task<IActionResult> GetBySchoolArea([FromQuery] long schoolAreaId)
    {
        try
        {
            var result = await _tagService.GetBySchoolArea(schoolAreaId);
            var dtos = result.Select(t => _mapper.Map<TagDto>(t))
                .ToList();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] TagDto tagDto)
    {
        try
        {
            await _tagService.AddOrUpdate(tagDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}