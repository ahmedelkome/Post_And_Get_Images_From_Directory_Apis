using Microsoft.AspNetCore.Mvc;

namespace ImagesApi.Controllers
{
    [ApiController]
    [Route("ImagesApis")]
    public class ImageController : ControllerBase
    {
        [HttpGet("ImageApis/{filename}")]
        public IActionResult GetImage(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory()+ @"\Images", filename);

            var fileByets = System.IO.File.ReadAllBytes(filepath);
            var extension = Path.GetExtension(filename).ToLowerInvariant();
            string contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream", // النوع الافتراضي لو غير معروف
            };
            return File(fileByets, contentType);
        }

        [HttpPost("upload")]
        public IActionResult UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected or is empty.");
            }


            var filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\Images", file.FileName);


            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok("File uploaded successfully.");
        }
    }
}
