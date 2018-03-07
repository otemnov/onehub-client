using System.Runtime.Serialization;

namespace OnehubClient.Models
{
	[DataContract]
	public class OnehubFolders
	{
		[DataMember(Name = "items")]
		public OnehubFolder[] Folders { get; set; }
	}
}