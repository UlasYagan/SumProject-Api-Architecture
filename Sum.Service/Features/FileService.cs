using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sum.Model.Feature;
using Sum.Service.Helper;

namespace Sum.Service.Features
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly FileUpload _fileUpload;
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileService(
            IConfiguration configuration,
            IOptions<FileUpload> fileUpload,
            IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _fileUpload = fileUpload.Value;
        }

        public void CreateFolder(string folder)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, $"{_fileUpload.BaseFolder}/{folder}");
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
        }

        public string CreateFile(string id, string folder, IFormFile file)
        {
            string fileName = "";
            if (file.Length > 0)
            {
                CreateFolder(folder);
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, $"{_fileUpload.BaseFolder}/{folder}");

                fileName = Help.Convertcharts(file.FileName);
                fileName = fileName.Replace(" ", "_");
                var name = fileName.Split(".");

                if (name.Length != 0)
                    fileName = id + "." + name[name.Length - 1];

                var path = Path.Combine(newPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }

            var imagePath = $"{_fileUpload.BaseFolder}/{folder}/{fileName}";
            return imagePath;
        }

        public void RemoveFile(string folder, string path)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            //string path = Path.Combine(webRootPath, p);

            //FileInfo file = new FileInfo(path);

            //if (file.Exists)
            //    file.Delete();
        }
    }
}