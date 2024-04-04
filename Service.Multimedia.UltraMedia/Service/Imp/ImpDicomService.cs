using Dicom;
using Dicom.Imaging;
using Service.Multimedia.UltraMedia.Const;
using Service.Multimedia.UltraMedia.Models;
using Service.Multimedia.UltraMedia.Repository;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Service.Multimedia.UltraMedia.Service.Imp
{
	public class ImpDicomService : IDicomService
	{
		private readonly ICloudStorageRepository _storageRepository;

		public ImpDicomService(ICloudStorageRepository storageRepository)
		{
			_storageRepository = storageRepository;
		}

		public async Task<Stream> ConvertDicomToImage(DicomFile dicomFile)
		{
			try
			{
				var pixelData = DicomPixelData.Create(dicomFile.Dataset);
				var width = pixelData.Width;
				var height = pixelData.Height;
				var bytes = pixelData.GetFrame(0).Data;

				var image = new Image<Rgba32>(width, height);

				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						var colorValue = bytes[y * width + x];
						image[x, y] = new Rgba32(colorValue, colorValue, colorValue);
					}
				}

				var outputStream = new MemoryStream();
				image.SaveAsPng(outputStream);
				outputStream.Position = 0; // Restablece la posición del stream para que pueda ser leído desde el principio

				// Guarda la imagen en tu PC
				string filePath = @"C:\Users\kevin\Downloads/imagen3.png";
				image.SaveAsPng(filePath);

				return outputStream;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return null;
		}

		public Task DownloadFileByTelephoneMetadada(long telephone)
		{
			throw new NotImplementedException();
		}

		public async Task<string> Facade_ToSaveAndSendImageOfUltrasound(IFormFile formFile, Study study)
		{
			DicomFile dicomFile = await GetDicomFileFromFile(formFile);
			Stream stream = await ConvertDicomToImage(dicomFile);
			string link = await UploadImageAsync(stream, dicomFile, study);

			return link;
		}

		public async Task<DicomFile> GetDicomFileFromFile(IFormFile file)
		{
			var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			memoryStream.Position = 0;
			DicomFile dicomFile = DicomFile.Open(memoryStream);

			return dicomFile;
		}

		public string GetDicomTagValue(DicomDataset dataset, DicomTag tag, string defaultValue)
		{
			return dataset.Contains(tag) ? dataset.GetString(tag) : defaultValue;
		}

		public async Task<string> UploadImageAsync(Stream stream, DicomFile dicomFile, Study study)
		{
			var options = new Object
			{
				Metadata = new Dictionary<string, string>
		{
			{Metadata.NAME_PATIENT, GetDicomTagValue(dicomFile.Dataset, DicomTag.PatientName, "Nombre desconocido") },
			{Metadata.ID_PATIENT, GetDicomTagValue(dicomFile.Dataset, DicomTag.PatientID, "ID desconocida") },
			{Metadata.BIRTH_DATE, GetDicomTagValue(dicomFile.Dataset, DicomTag.PatientBirthDate, "Fecha de nacimiento desconocida") },
			{Metadata.CREATED_DATE, GetDicomTagValue(dicomFile.Dataset, DicomTag.InstanceCreationDate, "Fecha de creación desconocida") },
			{Metadata.FILESET_CREATEDATE, GetDicomTagValue(dicomFile.Dataset, DicomTag.SeriesDate, "Fecha de la serie desconocida") },
			{Metadata.FILESET_CREATOR, GetDicomTagValue(dicomFile.Dataset, DicomTag.Manufacturer, "Fabricante desconocido") },
			{Metadata.FILESET_CONTENT, GetDicomTagValue(dicomFile.Dataset, DicomTag.SeriesDescription, "Descripción de la serie desconocida") },
			{Metadata.ID_SERVICE, study.Id_Service.ToString() },
			{Metadata.RSV, study.RSV},
			{Metadata.SERVICE, study.Service},
		},
				Name = study.Name_Ultrasound,
				ContentType = "image/png"
			};

			//string link = await _storageRepository.UploadImageAsync(stream, options);

			return "link";
		}
	}
}

