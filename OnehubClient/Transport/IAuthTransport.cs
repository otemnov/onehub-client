using System;
using System.Threading.Tasks;
using OnehubClient.Models;

namespace OnehubClient.Transport
{
	public interface IAuthTransport : IDisposable
	{
		Task<OnehubAuthToken> RequestAuthToken(OnehubCredentials onehubCredentials);
	}
}
