using System.IO;

namespace ViajanteApi.Common.Helpers
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
        bool UploadPhoto(MemoryStream stream, string folder, string name);
    }
}
