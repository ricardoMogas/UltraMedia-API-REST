using Service.Multimedia.UltraMedia.Models;

namespace Service.Multimedia.UltraMedia.DTO
{
	public class UploadViewModel
	{
		public IFormFile File { get; set; }
		public Study Study { get; set; }
	}
}
