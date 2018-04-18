using System;
using System.Threading.Tasks;
using OnehubClient.Models;
using OnehubClient.Transport;

namespace OnehubClient
{
	public class OnehubAuthorization : IOnehubAuthorization
	{
		private readonly IAuthtokenStore _tokenStore;
		private readonly IAuthTransport _transport;

		public OnehubAuthorization(IAuthtokenStore tokenStore, IAuthTransport transport)
		{
			_tokenStore = tokenStore ?? throw new ArgumentNullException(nameof(tokenStore));
			_transport = transport ?? throw new ArgumentNullException(nameof(transport));
		}

		public async Task AuthorizeAsync(IApiTransport transport, OnehubCredentials credentials)
		{
			var onehubAuthToken = await _tokenStore.GetTokenAsync(credentials);
			if (shouldRequestToken(onehubAuthToken))
			{
				onehubAuthToken = await _transport.RequestAuthToken(credentials);
				await _tokenStore.StoreTokenAsync(credentials, onehubAuthToken);
			}
			transport.ApplyAuthentication(onehubAuthToken);
		}

		public async Task ReAuthorizeAsync(IApiTransport transport, OnehubCredentials credentials)
		{
			var onehubAuthToken = await _transport.RequestAuthToken(credentials);
			await _tokenStore.StoreTokenAsync(credentials, onehubAuthToken);
			transport.ApplyAuthentication(onehubAuthToken);
		}

		private static bool shouldRequestToken(OnehubAuthToken onehubAuthToken)
		{
			return onehubAuthToken == null || onehubAuthToken.AccessTokenExpirationUtc > DateTime.UtcNow;
		}
	}
}