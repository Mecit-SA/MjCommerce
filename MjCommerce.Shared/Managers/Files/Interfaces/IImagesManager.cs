using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Managers.Files.Interfaces
{
    public interface IImagesManager
    {
        FileStreamResult Get(string name);
        Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> formFiles);
        Task<string> DeleteAsync(string name);
    }
}