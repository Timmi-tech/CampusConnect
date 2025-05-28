using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using ChatSystem_1.Application.Services.Contracts;
using ChatSystem_1.Application.DTOs;

[ApiController]
[Route("api/images")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    /// <summary>
    /// Uploads an image to Cloudinary.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <returns>The uploaded image result.</returns>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]  // ✅ Ensure Swagger knows it's a file upload

    [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
    )]
    public async Task<IActionResult> Upload([FromForm] FileUploadDto uploadDto)
    {
        if (uploadDto.File == null || uploadDto.File.Length == 0)
            return BadRequest(new { message = "No file uploaded or file is empty." });

        try
        {
            var result = await _photoService.UploadImageAsync(uploadDto.File);

            if (result.Error != null)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new
            {
                message = "Image uploaded successfully.",
                publicId = result.PublicId,
                url = result.SecureUrl.ToString()
            });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while uploading the image.", error = ex.Message });
        }
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return BadRequest(new { message = "Invalid Public ID." });

        try
        {
            var result = await _photoService.DeleteImageAsync(publicId);

            if (result.Error != null)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new { message = "Image deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while deleting the image.", error = ex.Message });
        }
    }
}
