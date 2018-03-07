using System;
using System.IO;
using OnehubClient;
using OnehubClient.Models;
using OnehubClient.Transport;
using Xunit;

namespace Tests
{
	public class Test
	{
		[Fact]
		public void IntegrationTest()
		{
			var client = new OnehubClient.OnehubClient(new OnehubAuthorization(new AuthtokenStore(), new AuthTransport()),
				new ApiTransport(),
				new OnehubCredentials
				{
					ClientId = Environment.GetEnvironmentVariable("onehub_id"),
					ClientSecret = Environment.GetEnvironmentVariable("onehub_secret"),
					Password = Environment.GetEnvironmentVariable("onehub_pwd"),
					Username = Environment.GetEnvironmentVariable("onehub_user")
				});
			var onehubFolder = client.CreateFolder(1149161, "ok").Result;
			var onehubFile = client.UploadFile(1149161, onehubFolder.Folder.Id.Value,
				new FileDescription {Name = "text.txt", MimeType = "text/plain", Stream = new MemoryStream(new byte[0])}).Result;
			Assert.NotNull(onehubFile);
		}
	}
}