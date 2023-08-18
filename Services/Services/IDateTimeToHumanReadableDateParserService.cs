using CGPost.Base.Interfaces;

namespace CGPost.Services.Services
{
    public interface IDateTimeToHumanReadableDateParserService : IService
    {
        string Parse(DateTime date);
    }
}
