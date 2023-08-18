using CGPost.Base.Interfaces;
using CGPost.Models.Tracking;

namespace CGPost.Services.Services
{
    public interface IPayloadTransformerService : IService
    {
        TrackingData? TransformToTrackingData(DocumentElement? documentElement);
    }
}
