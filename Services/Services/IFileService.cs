using CGPost.Base.Interfaces;

namespace CGPost.Services.Services
{
    public interface IFileService : IService
    {
        public Task<Stream> OpenPackageFile(string resourcePath);
    }
}
