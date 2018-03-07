using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace OnehubClient.Transport
{
	public class JsonContent : ByteArrayContent
	{
		public JsonContent(object content) : this(content, null)
		{
		}

		public JsonContent(object content, Encoding encoding) : base(GetContentByteArray(content, encoding))
		{
			Headers.ContentType = new MediaTypeHeaderValue("application/json")
			{
				CharSet = encoding?.WebName ?? Encoding.UTF8.WebName
			};
		}

		private static byte[] GetContentByteArray(object content, Encoding encoding)
		{
			if (content == null)
				throw new ArgumentNullException(nameof(content));
			if (encoding == null)
				encoding = Encoding.UTF8;
			string json = JsonConvert.SerializeObject(content);
			return encoding.GetBytes(json);
		}
	}
}