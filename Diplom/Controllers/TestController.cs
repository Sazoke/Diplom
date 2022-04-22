using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Test;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[Route("[controller]/[action]")]
public class TestController : Controller
{
    private readonly ITestService _testService;
    private readonly IMapper _mapper;

    public TestController(ITestService testService, IMapper mapper)
    {
        _testService = testService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetById([FromQuery] long id)
    {
        try
        {
            var test = _testService.GetById(id);
            var dto = _mapper.Map<TestDto>(test);
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public IActionResult GetQuestionsById([FromQuery] long id)
    {
        try
        {
            var test = _testService.GetById(id);
            return Ok(test.Questions);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdate([FromBody] TestEditDto testDto)
    {
        try
        {
            await _testService.AddOrUpdateAsync(testDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult GetByFilter([FromBody] Filter filter)
    {
        try
        {
            var tests = _testService.GetByFilter(filter);
            var dtos = tests.Select(_mapper.Map<TestPreviewDto>).ToList();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Remove([FromQuery] long id)
    {
        try
        {
            await _testService.RemoveAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}