
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Service.Multimedia.UltraMedia.Const;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Service.Multimedia.UltraMedia.Repository.Imp
{
	public class ImpCloudStorageRepository : ICloudStorageRepository
	{
		private readonly GoogleCredential _credential;
		private readonly StorageClient _storageClient;
		private readonly string bucketName;

		public ImpCloudStorageRepository(IConfiguration configuration)
		{
			_credential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredetialsFile"));
			_storageClient = StorageClient.Create(_credential);
			bucketName = configuration.GetValue<string>("GoogleCloud_Name_Bucket");
		}
		public Task DeleteFileAsync(string fileNameForStorage)
		{
			throw new NotImplementedException();
		}

		public async Task GetFilebByTelephone(int patientId)
		{
			var files = _storageClient.ListObjects(bucketName);

			foreach (var obj in files)
			{
				if (obj.Metadata != null && obj.Metadata.ContainsKey(Metadata.ID_PATIENT) && obj.Metadata[Metadata.ID_PATIENT] == patientId.ToString())
				{
					var localPath = Path.Combine("C:\\Users\\kevin\\Downloads", obj.Name);
					using var outputFile = File.OpenWrite(localPath);
					await _storageClient.DownloadObjectAsync(obj, outputFile);
					return;
				}
			}
		}

		public async Task<string> UploadImageAsync(Stream imageStream, Object objectOptions)
		{
			objectOptions.Bucket = bucketName;
			var dataObject = await _storageClient.UploadObjectAsync(objectOptions, imageStream);

			return dataObject.MediaLink;
		}
	}
}
