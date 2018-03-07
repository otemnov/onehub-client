using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OnehubClient.Models;

namespace OnehubClient.Transport
{
	public class AuthTransport : IAuthTransport
	{
		private readonly HttpClient _authHttpClient = new HttpClient();

		static AuthTransport()
		{
			//required for onehub
			ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
		}

		public async Task<OnehubAuthToken> RequestAuthToken(OnehubCredentials onehubCredentials)
		{
			HttpResponseMessage authMessage = await _authHttpClient.PostAsync(new Uri("https://ws-api.onehub.com/oauth/token"), toFormUrlEncodedContent(onehubCredentials));
			DateTime now = DateTime.UtcNow;
			JObject authObject =  JObject.Parse(await authMessage.Content.ReadAsStringAsync());
			return new OnehubAuthToken
			{
				AccessToken = authObject["access_token"].Value<string>(),
				RefreshToken = authObject["refresh_token"].Value<string>(),
				AccessTokenIssueDateUtc = now,
				AccessTokenExpirationUtc = now.AddSeconds(authObject["expires_in"].Value<long>())
			};
		}
		
		private static FormUrlEncodedContent toFormUrlEncodedContent(OnehubCredentials onehubCredentials)
		{
			var postData = new Dictionary<string, string>
			{
				["client_id"] = onehubCredentials.ClientId,
				["client_secret"] = onehubCredentials.ClientSecret,
				["username"] = onehubCredentials.Username,
				["password"] = onehubCredentials.Password,
				["grant_type"] = "password"
			};
			return new FormUrlEncodedContent(postData);
		}
		
		public void Dispose()
		{
			_authHttpClient.Dispose();
		}
	}
}