using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnehubClient.Models;

namespace OnehubClient.Transport
{
	public interface IApiTransport : IDisposable
	{
		void ApplyAuthentication(OnehubAuthToken onehubAuthToken);
		Task<OnehubFolder> CreateFolder(long workspaceId, string name);
		Task<OnehubFile> UploadFile(long workspaceId, long folderId, FileDescription file);
		Task<OnehubFolders> GetFolders(long workspaceId, int offset);
		Task<OnehubFolder> GetRootFolder(long workspaceId);
	}
}