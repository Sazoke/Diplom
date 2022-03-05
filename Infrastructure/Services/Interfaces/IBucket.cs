using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IBucket
{
    Task<byte[]> ReadFileAsync(string fileName);

    Task<string> WriteFileAsync(IFormFile formFile);
    
    byte[] ReadFile(string fileName);

    string WriteFile(IFormFile formFile);
}