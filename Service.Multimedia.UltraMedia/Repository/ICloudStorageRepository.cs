using Object = Google.Apis.Storage.v1.Data.Object;

namespace Service.Multimedia.UltraMedia.Repository
{
	public interface ICloudStorageRepository
	{
		Task<string> UploadImageAsync(Stream imageStream, Object objectOptions);
		Task DeleteFileAsync(string fileNameForStorage);
		Task GetFilebByTelephone(int patienrId);
	}
}
