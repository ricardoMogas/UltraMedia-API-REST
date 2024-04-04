using System.ComponentModel.DataAnnotations;

namespace Service.Multimedia.UltraMedia.Models
{
	public class Study
	{
		[Required]
		public int Id_Service { get; set; }
		[Required]
		public string Name_Ultrasound { get; set; }
		[Required]
		public string RSV { get; set; }
		[Required]
		public string Service { get; set; }
	}
}
