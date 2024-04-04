using FellowOakDicom;
using Service.Multimedia.UltraMedia.Models;

namespace Service.Multimedia.UltraMedia.Service
{
	public interface IDicomService
	{
		Task<DicomFile> GetDicomFileFromFile(IFormFile file);
		Task<string> UploadImageAsync(Stream stream, DicomFile dicomFile, Study study);
		Task<Stream> ConvertDicomToImage(DicomFile dicomFile);
		Task DownloadFileByTelephoneMetadada(long telephone);
		Task<string> Facade_ToSaveAndSendImageOfUltrasound(IFormFile formFile, Study study);
		public string GetDicomTagValue(DicomDataset dataset, DicomTag tag, string defaultValue);
	}
}
