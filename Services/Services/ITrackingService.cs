using CGPost.Base.Interfaces;
using CGPost.Models.Tracking;

namespace CGPost.Services.Services
{
    public interface ITrackingService : IService
    {
        Task<TrackingData?> GetTrackingInfo(string trackingNumber);
    }
}
