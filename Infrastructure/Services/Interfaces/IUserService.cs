using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateUserAsync(CreateUserDto createUserDto);

    Task<ApplicationUser> GetProfile(string id);

    Task UpdateProfile(ProfileEditDto profileEditDto);

    IEnumerable<ApplicationUser> GetByFilter(Filter filter);

    Task LoginAsync(LoginDto loginDto);

    Task LogoutAsync();
}