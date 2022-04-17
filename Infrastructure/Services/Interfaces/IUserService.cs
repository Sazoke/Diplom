using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateUserAsync(CreateUserDto userDto);

    Task<ApplicationUser> GetProfile(string id);

    Task UpdateProfile(ProfileEditDto profileEditDto);
}