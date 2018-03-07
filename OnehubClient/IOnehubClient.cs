using System.Threading.Tasks;
using OnehubClient.Models;

namespace OnehubClient
{
	public interface IOnehubClient
	{
		Task<OnehubFolder> CreateFolder(long workspaceId, string name);
		Task<OnehubFile> UploadFile(long workspaceId, long folderId, FileDescription file);
	}
}