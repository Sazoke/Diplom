using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[Route("[controller]/[action]")]
[Authorize]
public class AttachmentController : Controller
{
    private readonly IBucket _bucket;

    public AttachmentController(IBucket bucket)
    {
        _bucket = bucket;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(IEnumerable<IFormFile> file)
    {
        try
        {
            var storageName = await _bucket.WriteFileAsync(file.First());
            return Ok(storageName);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete]
    [Authorize]
    public IActionResult Delete(string name)
    {
        try
        {
            _bucket.Remove(name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}