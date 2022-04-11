using System;
using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[Route("[controller]/[action]")]
public class AttachmentController : Controller
{
    private readonly IBucket _bucket;

    public AttachmentController(IBucket bucket)
    {
        _bucket = bucket;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] IFormFile file)
    {
        try
        {
            var storageName = await _bucket.WriteFileAsync(file);
            return Ok(storageName);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] string name)
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