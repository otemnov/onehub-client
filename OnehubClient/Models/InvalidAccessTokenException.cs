using System;
using System.Runtime.Serialization;

namespace OnehubClient.Models
{
	[Serializable]
	public class InvalidAccessTokenException : Exception
	{
		public InvalidAccessTokenException()
		{
		}

		public InvalidAccessTokenException(string message) : base(message)
		{
		}

		public InvalidAccessTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidAccessTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}