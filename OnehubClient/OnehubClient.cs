using System;
using System.Linq;
using System.Net.Http;
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
			return await authorizeAction(() => createFolder(workspaceId, name));
		}

		private async Task<OnehubFolder> createFolder(long workspaceId, string name)
		{
			var onehubFolder = await _transport.CreateFolder(workspaceId, name);
			var errors = onehubFolder.Folder.Errors;
			if (errors?.Filename != null && errors.Filename.Any())
			{
				var folders = await _transport.GetFolders(workspaceId);
				onehubFolder = folders.Folders.FirstOrDefault(x => string.Equals(x.Folder.SanitizedName, onehubFolder.Folder.SanitizedName, StringComparison.OrdinalIgnoreCase));
			}

			return onehubFolder;
		}

		public async Task<OnehubFile> UploadFile(long workspaceId, long folderId, FileDescription file)
		{
			return await authorizeAction(() => _transport.UploadFile(workspaceId, folderId, file));
		}

		private async Task<TResult> authorizeAction<TResult>(Func<Task<TResult>> action)
		{
			await _authorization.AuthorizeAsync(_transport, _credentials);
			try
			{
				return await action();
			}
			catch (InvalidAccessTokenException)
			{
				await _authorization.ReAuthorizeAsync(_transport, _credentials);
			} 
			return await action();
		}

	}
}