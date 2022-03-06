using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diplom.Dtos.Activity;
using Diplom.Dtos.Material;
using Diplom.Dtos.User;
using Duende.IdentityServer.Extensions;
using Infrastructure.Models.Application;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly ApplicationUserManager _userManager;
    private readonly IMapper _mapper;
    private readonly IBucket _bucket;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(ApplicationUserManager userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, IBucket bucket)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _bucket = bucket;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateUserDto userDto)
    {
        var user = _mapper.Map<ApplicationUser>(userDto);
        user.UserName = user.Email;
        user.Image = await _bucket.WriteFileAsync(userDto.Image);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Succeeded)
            await _signInManager.SignInAsync(user, false);
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile([FromQuery] string id)
    {
        if (id is null && !User.IsAuthenticated())
            return NotFound();
        var user = id is null ? _userManager.GetUserWithComponents(User) : _userManager.GetUserWithComponents(id);
        var dto = _mapper.Map<UserProfileDto>(user);
        //dto.Image = await _bucket.ReadFileAsync(user.Image);
        var countOfComponents = id is null ? 3 : 4;
        dto.Activities = user.Activities.Take(countOfComponents)
            .Select(_mapper.Map<PreviewActivityDto>)
            .ToList();
        dto.Materials = user.Materials.Take(countOfComponents)
            .Select(_mapper.Map<PreviewMaterialDto>)
            .ToList();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(_bucket.ReadFile("055f73e6caf946c287da7db3a8bb4bdf.jpg"));
    }
}