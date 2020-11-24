using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MjCommerce.Shared.Managers.Files.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Managers.Files
{
    public class ImagesManager : IImagesManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DateTime _currentUtcDateTime = DateTime.UtcNow;

        public ImagesManager(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> formFiles)
        {
            ICollection<string> fileNames = new List<string>();

            if (formFiles != null && formFiles.Any())
            {
                foreach (IFormFile formFile in formFiles)
                {
                    fileNames.Add(await SingleUploadAsync(formFile));
                }
            }

            return fileNames;
        }

        public FileStreamResult Get(string name)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", name);

            if (!File.Exists(path))
            {
                return null;
            }

            var bytes = File.ReadAllBytes(path);

            var stream = new MemoryStream(bytes);

            return new FileStreamResult(stream, "application/octet-stream");
        }

        public Task<string> DeleteAsync(string name)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", name);

            if (!File.Exists(path))
            {
                return Task.FromResult(string.Empty);
            }

            File.Delete(path);
            return Task.FromResult(name);
        }


        async Task<string> SingleUploadAsync(IFormFile formFile)
        {
            string fileName = GenerateRandomName(formFile.FileName);

            string outputDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            string imageFullPath = Path.Combine(outputDirectory, fileName);

            return await Compress(formFile, 90, imageFullPath);
        }

        Task<string> Compress(IFormFile formFile, byte quality, string imageFullPath)
        {
            using (Bitmap bitmap = new Bitmap(formFile.OpenReadStream()))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                Encoder myEncoder = Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, (long)quality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                bitmap.Save(imageFullPath, jpgEncoder, myEncoderParameters);

                return Task.FromResult(Path.GetFileName(imageFullPath));
            }
        }

        ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        string GenerateRandomName(string name)
        {
            return string.Join("_", Guid.NewGuid().ToString(),
                                    _currentUtcDateTime.Year,
                                    _currentUtcDateTime.Month,
                                    _currentUtcDateTime.Day,
                                    _currentUtcDateTime.Hour,
                                    _currentUtcDateTime.Minute,
                                    _currentUtcDateTime.Second,
                                    _currentUtcDateTime.Millisecond,
                                    name);
        }
    }
}