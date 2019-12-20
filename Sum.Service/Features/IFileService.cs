using Microsoft.AspNetCore.Http;

namespace Sum.Service.Features
{
    public interface IFileService
    {
        void CreateFolder(string folder);
        string CreateFile(string fileName, string folder, IFormFile file);
        void RemoveFile(string folder, string path);
    }
}