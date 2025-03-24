using AIJobCareer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIJobCareer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IR2FileService _fileService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IR2FileService fileService, ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string folder = "")
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was provided or file is empty");
            }

            try
            {
                var fileKey = await _fileService.UploadFileAsync(file, folder);
                return Ok(new { FileKey = fileKey });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during file upload: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading the file");
            }
        }


        [HttpGet("{fileKey}")]
        public async Task<IActionResult> GetFile(string fileKey)
        {
            try
            {
                var fileBytes = await _fileService.RetrieveFileAsync(fileKey);

                // Try to determine content type based on file extension
                var fileExtension = System.IO.Path.GetExtension(fileKey).ToLowerInvariant();
                var contentType = GetContentType(fileExtension);

                return File(fileBytes, contentType, System.IO.Path.GetFileName(fileKey));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving file: {ex.Message}");
                return NotFound($"File not found or error retrieving file");
            }
        }

        [HttpDelete("{fileKey}")]
        public async Task<IActionResult> DeleteFile(string fileKey)
        {
            try
            {
                var result = await _fileService.DeleteFileAsync(fileKey);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting file: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the file");
            }
        }

        private string GetContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".zip":
                    return "application/zip";
                case ".txt":
                    return "text/plain";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
