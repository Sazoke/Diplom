using Infrastructure.Dtos.User;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateUserAsync(CreateUserDto userDto);

    Task<UserProfileDto> GetProfile(string id);

    Task EditDescription(string description);

    Task<string> EditImage(IFormFile formFile);
}