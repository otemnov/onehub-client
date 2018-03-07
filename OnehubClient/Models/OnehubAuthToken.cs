using System;

namespace OnehubClient.Models
{
	public class OnehubAuthToken
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime? AccessTokenExpirationUtc { get; set; }
		public DateTime? AccessTokenIssueDateUtc { get; set; }
	}
}