using Dicom;
using Microsoft.AspNetCore.Mvc;
using Service.Multimedia.UltraMedia.DTO;
using Service.Multimedia.UltraMedia.Service;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.Net;

namespace Service.Multimedia.UltraMedia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IDicomService _dicomService;

		public FilesController(IDicomService dicom)
		{
			_dicomService = dicom;
		}


		/// <summary>
		/// Subir una imagen al almacenamiento en la nube
		/// </summary>
		/// <returns>Link para descargar el archivo</returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Upload([FromForm] UploadViewModel uploadView)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				string link = await _dicomService.Facade_ToSaveAndSendImageOfUltrasound(uploadView.File, uploadView.Study);

				/*	if (file.Length <= 0 || file == null)
					{
						return BadRequest("No image file provided");
					}*/

				response.Success = true;
				response.Message = "Image upload succesffully";
				response.Data = link;
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}

			return Ok(response);
		}

		[HttpPost("convert")]
		public async Task<IActionResult> ConvertDicomToPng(IFormFile dicomFile)
		{
			using (var ms = new MemoryStream())
			{
				dicomFile.CopyTo(ms);
				var dicomImage = new DicomImage(DicomFile.Open(ms).Dataset);
				var renderedImage = dicomImage.RenderImage();

				// Convertir el DicomImage a una imagen ImageSharp
				var image = Image.LoadPixelData<Rgba32>(renderedImage.Pixels.Data, renderedImage.Width, renderedImage.Height);

				// Guardar la imagen en el sistema de archivos local
				var filePath = Path.Combine("C:\\ruta\\a\\tu\\directorio", "imagen.png");
				image.Save(filePath, new PngEncoder());

				return Ok("Imagen guardada correctamente");
			}

		}

		/// <summary>
		/// Descargar un archivo 
		/// </summary>
		/// <returns></returns>
		/// <response code="200"> Exito </response>
		/// <response code="500">Ha ocurrido un error en la verificación.</response>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Download(long telephonePatient)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				await _dicomService.DownloadFileByTelephoneMetadada(telephonePatient);

				response.Success = true;
				response.Message = "Download succesffully";
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					response.Message = ex.Message); ;
			}

			return Ok(response);
		}
	}
}
