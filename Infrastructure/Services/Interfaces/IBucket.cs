using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IBucket
{
    Task<Stream> ReadFileAsync(string fileName);

    Task<string> WriteFileAsync(IFormFile formFile);
    
    Stream ReadFile(string fileName);

    string WriteFile(IFormFile formFile);

    void Remove(string name);
}