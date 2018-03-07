using System.Runtime.Serialization;

namespace OnehubClient.Models
{
	[DataContract]
	public class OnehubItem
	{
		[DataMember(Name = "id", IsRequired = false)]
		public long? Id { get; set; }
		[DataMember(Name = "filename", IsRequired = true)]
		public string Name { get; set; }
		[DataMember(Name = "ancestor_ids", IsRequired = false)]
		public long[] AncestorIds { get; set; }
		[DataMember(Name = "workspace_id", IsRequired = false)]
		public long WorkspaceId { get; set; }
		[DataMember(Name = "sanitized_filename", IsRequired = false)]
		public string SanitizedName { get; set; }
		[DataMember(Name = "errors", IsRequired = false)]
		public OnehubError Errors { get; set; }
	}

	[DataContract]
	public class OnehubError
	{
		[DataMember(Name = "filename", IsRequired = false)]
		public string[] Filename { get; set; }
	}

}