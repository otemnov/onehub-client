using System.Threading.Tasks;
using OnehubClient.Models;

namespace OnehubClient
{
	public interface IAuthtokenStore
	{
		Task<OnehubAuthToken> GetTokenAsync(OnehubCredentials onehubCredentials);
		Task StoreTokenAsync(OnehubCredentials onehubCredentials, OnehubAuthToken token);
	}

	public class AuthtokenStore : IAuthtokenStore
	{
		private OnehubAuthToken _token;

		public Task<OnehubAuthToken> GetTokenAsync(OnehubCredentials onehubCredentials)
		{
			return Task.FromResult(_token);
		}

		public Task StoreTokenAsync(OnehubCredentials onehubCredentials, OnehubAuthToken token)
		{
			_token = token;
			return Task.CompletedTask;
		}
	}
}