using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(SignInManager<ApplicationUser> signInManager, DbContext dbContext, IMapper mapper, ApplicationContext applicationContext, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<string> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<ApplicationUser>(createUserDto);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (result.Succeeded)
            await _signInManager.SignInAsync(user, false);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task LoginAsync(LoginDto loginDto)
    {
        var normalizedName = loginDto.Name.Normalize();
        var user = _dbContext.Set<ApplicationUser>()
            .FirstOrDefault(u => u.NormalizedUserName == normalizedName || u.NormalizedEmail == normalizedName);
        if (user is null)
            throw new Exception("Wrong login");
        var result =
            await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe,
                lockoutOnFailure: false);
        if (result.Succeeded)
            throw new Exception("Wrong password");
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();

    public async Task<ApplicationUser> GetProfile(string id)
    {
        var user = _dbContext.Set<ApplicationUser>()
            .Include(u => u.Materials)
            .Include(u => u.Activities)
            .FirstOrDefault(u => u.Id == id);
        return user;
    }

    public async Task UpdateProfile(ProfileEditDto profileEditDto)
    {
        var user = _dbContext.Set<ApplicationUser>()
            .FirstOrDefault(u => u.Id == profileEditDto.Id);
        user.Name = profileEditDto.Name;
        user.Description = profileEditDto.Description;
        user.Image = profileEditDto.Image;
        user.SchoolAreaId = profileEditDto.SchoolAreaId;
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<ApplicationUser> GetByFilter(Filter filter)
    {
        var users = _dbContext.Set<ApplicationUser>()
            .Where(u => (filter.Text == null || u.Name.Contains(filter.Text)) &&
                        (filter.SchoolArea == null || u.SchoolAreaId == filter.SchoolArea)).Skip(filter.Skip)
            .Take(filter.PageSize);
        return users;
    }
}