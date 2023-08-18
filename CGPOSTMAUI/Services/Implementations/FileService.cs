using CGPost.Services.Services;

namespace CGPost.Services
{
    public class FileService : IFileService
    {
        public Task<Stream> OpenPackageFile(string resourcePath)
        {
            return FileSystem.OpenAppPackageFileAsync(resourcePath);
        }
    }
}
