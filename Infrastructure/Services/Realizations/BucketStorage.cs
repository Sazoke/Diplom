using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Realizations;

public class BucketStorage : IBucket
{
    private readonly string _directory;

    public BucketStorage()
    {
        _directory = $"{Directory.GetCurrentDirectory()}\\Files";
        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);
    }

    public async Task<byte[]> ReadFileAsync(string fileName)
    {
        var path = GetFullPath(fileName);
        var bytes = await File.ReadAllBytesAsync(path);
        
        //var stream = new MemoryStream(bytes);
        //var formFile = new FormFile(stream, 0, bytes.Length, fileName, path);
        return bytes;
    }

    public async Task<string> WriteFileAsync(IFormFile formFile)
    {
        var fileName = GenerateUniqueFileName(formFile);
        var fullPath = GetFullPath(fileName);
        
        var dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        
        var file = File.OpenWrite(fullPath);
        await formFile.CopyToAsync(file);

        return fileName;
    }

    public byte[] ReadFile(string fileName)
    {
        var path = GetFullPath(fileName);
        var bytes = File.ReadAllBytes(path);
        //var ms = new MemoryStream(bytes);
        //var file = new FormFile(ms, 0, bytes.Length, fileName, fileName);
        return bytes;
    }

    public string WriteFile(IFormFile formFile)
    {
        var fileName = GenerateUniqueFileName(formFile);
        var fullPath = GetFullPath(fileName);
        
        var dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        
        var file = File.OpenWrite(fullPath);
        formFile.CopyTo(file);

        return fileName;
    }

    private string GetFullPath(string fileName) => $"{_directory}\\{fileName}";

    private static string GenerateUniqueFileName(IFormFile formFile)
    {
        return Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName).ToLower();
    }
}