using System.IO;

namespace OnehubClient.Models
{
	public class FileDescription
	{
		public Stream Stream { get; set; }
		public string Name { get; set; }
		public string MimeType { get; set; }
	}
}