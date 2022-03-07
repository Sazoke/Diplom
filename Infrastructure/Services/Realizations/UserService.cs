using AutoMapper;
using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Material;
using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Services.Realizations;

public class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationContext _applicationContext;
    private readonly DbContext _dbContext;
    private readonly IBucket _bucket;
    private readonly IMapper _mapper;

    public UserService(SignInManager<ApplicationUser> signInManager, DbContext dbContext, IBucket bucket, IMapper mapper, ApplicationContext applicationContext, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
        _bucket = bucket;
        _mapper = mapper;
        _applicationContext = applicationContext;
        _userManager = userManager;
    }

    public async Task<string> CreateUserAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<ApplicationUser>(userDto);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Succeeded)
            await _signInManager.SignInAsync(user, false);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<UserProfileDto> GetProfile(string id)
    {
        var countOfComponents = id is null ? 3 : 4;
        id ??= _applicationContext.CurrentUserId;

        var user = _dbContext.Set<ApplicationUser>()
            .Include(u => u.Activities)
            .Include(u => u.Materials)
            .Include(u => u.Tests)
            .ThenInclude(t => t.Questions)
            .ThenInclude(q => q.Options)
            .Include(u => u.Tests)
            .ThenInclude(t => t.Results)
            .FirstOrDefault(u => u.Id == id);
        
        var dto = _mapper.Map<UserProfileDto>(user);
        dto.Activities = user.Activities.Take(countOfComponents)
            .Select(_mapper.Map<PreviewActivityDto>)
            .ToList();
        dto.Materials = user.Materials.Take(countOfComponents)
            .Select(_mapper.Map<PreviewMaterialDto>)
            .ToList();

        return dto;
    }

    public async Task EditDescription(string description)
    {
        var user = _dbContext.Set<ApplicationUser>().FirstOrDefault(u => u.Id == _applicationContext.CurrentUserId);
        user.Description = description;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> EditImage(IFormFile formFile)
    {
        var user = _dbContext.Set<ApplicationUser>().FirstOrDefault(u => u.Id == _applicationContext.CurrentUserId);
        var fileName = await _bucket.WriteFileAsync(formFile);
        user.Image = fileName;
        await _dbContext.SaveChangesAsync();
        return fileName;
    }
}