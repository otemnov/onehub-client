using System.Runtime.Serialization;

namespace OnehubClient.Models
{
	[DataContract]
	public class OnehubFolder
	{
		[DataMember(Name = "folder")]
		public OnehubItem Folder { get; set; }
	}
}