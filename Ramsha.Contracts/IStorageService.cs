using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramsha.Application.Contracts;

public interface IStorageService
{
	Task<string?> UploadFile(byte[] file, string fileName, string contentType, string folderPath = null);
	Task<bool> DeleteFile(string path);
	string GetImageUrl(string objectPath);
}
