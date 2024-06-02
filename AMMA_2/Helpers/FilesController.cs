using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AMMAAPI.Helpers
{
    [Authorize]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filename = await WriteFile(file);

            return Ok(new { Filename = filename });
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                filename = Path.GetFileName(file.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            return filename;
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        [HttpGet]
        [Route("GetImage")]
        public async Task<IActionResult> GetImage(string filename)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(imagePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            return File(bytes, contentType);
        }

    }
}
