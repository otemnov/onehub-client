using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnehubClient.Models;

namespace OnehubClient.Transport
{
	public class ApiTransport : IApiTransport
	{
		private readonly HttpClient _apiHttpClient = new HttpClient();

		public ApiTransport()
		{
			_apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public void ApplyAuthentication(OnehubAuthToken onehubAuthToken)
		{
			_apiHttpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", onehubAuthToken.AccessToken);
		}

		public async Task<OnehubFolder> CreateFolder(long workspaceId, string name)
		{
			var httpResponse = await _apiHttpClient.PostAsync(
				new Uri($"https://ws-api.onehub.com/workspaces/{workspaceId}/folders"),
				new JsonContent(new {folder = new {filename = name}}));
			httpResponse.EnsureAuthorized();
			var response = await httpResponse.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OnehubFolder>(response);
		}

		public async Task<OnehubFile> UploadFile(long workspaceId, long folderId, FileDescription file)
		{
			var streamContent = new StreamContent(file.Stream);
			streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = "\"file\"",
				FileName = "\"" + file.Name + "\""
			};
			streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.MimeType);

			string boundary = Guid.NewGuid().ToString();
			var content = new MultipartFormDataContent(boundary);
			content.Headers.Remove("Content-Type");
			content.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
			content.Add(streamContent);

			var httpResponse = await _apiHttpClient.PostAsync(new Uri($"https://ws-api.onehub.com/workspaces/{workspaceId}/folders/{folderId}/files"), content);
			httpResponse.EnsureAuthorized();
			var response = await httpResponse.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OnehubFile>(response);
		}

		public async Task<OnehubFolders> GetFolders(long workspaceId)
		{
			var rootFolder = await GetRootFolder(workspaceId);
			var httpResponse =
				await _apiHttpClient.GetAsync(new Uri($"https://ws-api.onehub.com/workspaces/{workspaceId}/folders/{rootFolder.Folder.Id}"));
			httpResponse.EnsureAuthorized();
			var response = await httpResponse.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OnehubFolders>(response);
		}

		public async Task<OnehubFolder> GetRootFolder(long workspaceId)
		{
			var httpResponse =
				await _apiHttpClient.GetAsync(new Uri($"https://ws-api.onehub.com/workspaces/{workspaceId}/folders"));
			var response = await httpResponse.Content.ReadAsStringAsync();
			httpResponse.EnsureAuthorized();
			return JsonConvert.DeserializeObject<OnehubFolders>(response).Folders[0];
		}

		public void Dispose()
		{
			_apiHttpClient.Dispose();
		}
	}
}