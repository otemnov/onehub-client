using System.Threading.Tasks;
using OnehubClient.Models;
using OnehubClient.Transport;

namespace OnehubClient
{
	public interface IOnehubAuthorization
	{
		Task AuthorizeAsync(IApiTransport transport,OnehubCredentials credentials);
		Task ReAuthorizeAsync(IApiTransport transport, OnehubCredentials credentials);
	}
}