using System.Runtime.Serialization;

namespace OnehubClient.Models
{
	[DataContract]
	public class OnehubFile
	{
		[DataMember(Name = "file", EmitDefaultValue = false, IsRequired = true)]
		public OnehubItem File { get; set; }
	}
}