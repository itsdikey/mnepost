using CGPost.Base.Interfaces;
using CGPost.Models.Enums;

namespace CGPost.Services
{
    public interface IToastService : IService
    {
        Task ShowToast(string message, ToastDurations duration);
    }
}
