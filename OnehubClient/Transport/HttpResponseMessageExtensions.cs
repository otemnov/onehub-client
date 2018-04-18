using System.Net;
using System.Net.Http;
using OnehubClient.Models;

namespace OnehubClient.Transport
{
	public static class HttpResponseMessageExtensions
	{
		public static void EnsureAuthorized(this HttpResponseMessage responseMessage)
		{
			if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new InvalidAccessTokenException();
			}
		}
	}
}