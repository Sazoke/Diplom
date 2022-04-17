using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Realizations;

public class BucketStorage : IBucket
{
    private readonly string _directory;

        public BucketStorage(string webRootPath)
        {
            _directory = $"{webRootPath}\\Files";
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);
        }

        public string WriteFile(IFormFile formFile)
        {
            var fileName = GenerateUniqueFileName(formFile);
            var fullPath = GetPath(fileName);
        
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        
            var file = File.OpenWrite(fullPath);
            formFile.CopyTo(file);
            file.Close();

            return fileName;
        }

        public void Remove(string name)
        {
            var fileName = GetPath(name);
            if(!Directory.Exists(fileName))
                return;
            File.Delete(fileName);
        }

        public async Task<string> WriteFileAsync(IFormFile formFile)
        {
            var fileName = GenerateUniqueFileName(formFile);
            var fullPath = GetPath(fileName);
        
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        
            var file = File.OpenWrite(fullPath);
            await formFile.CopyToAsync(file);
            file.Close();

            return fileName;
        }

        public Stream ReadFile(string fileName)
        {
            var path = GetPath(fileName);
            var stream = File.OpenRead(path);
            return stream;
        }

        public async Task<Stream> ReadFileAsync(string fileName)
        {
            var path = GetPath(fileName);
            var stream = File.OpenRead(path);
            return stream;
        }

        private string GetPath(string fileName) => $"{_directory}\\{fileName}";

        private static string GenerateUniqueFileName(IFormFile formFile) =>
            Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName).ToLower();
}