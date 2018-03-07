using System;
using System.Linq;
using System.Threading.Tasks;
using OnehubClient.Models;
using OnehubClient.Transport;

namespace OnehubClient
{
	public class OnehubClient : IOnehubClient
	{
		private readonly IOnehubAuthorization _authorization;
		private readonly OnehubCredentials _credentials;
		private readonly IApiTransport _transport;

		public OnehubClient(IOnehubAuthorization authorization, IApiTransport transport, OnehubCredentials credentials)
		{
			_authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
			_transport = transport ?? throw new ArgumentNullException(nameof(transport));
			_credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
		}

		public async Task<OnehubFolder> CreateFolder(long workspaceId, string name)
		{
			await _authorization.AuthorizeAsync(_transport, _credentials);
			var onehubFolder = await _transport.CreateFolder(workspaceId, name);
			var errors = onehubFolder.Folder.Errors;
			if (errors?.Filename != null && errors.Filename.Any())
			{
				var folders = await _transport.GetFolders(workspaceId);
				onehubFolder = folders.Folders.FirstOrDefault(x => x.Folder.SanitizedName == onehubFolder.Folder.SanitizedName);
			}

			return onehubFolder;
		}

		public async Task<OnehubFile> UploadFile(long workspaceId, long folderId, FileDescription file)
		{
			await _authorization.AuthorizeAsync(_transport, _credentials);
			return await _transport.UploadFile(workspaceId, folderId, file);
		}
	}
}