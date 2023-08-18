using CGPost.Models.Enums;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CGPost.Services.Implementations
{
    public sealed class ToastService : IToastService
    {
        public async Task ShowToast(string message, ToastDurations duration)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            double fontSize = 14;
            
            var toast = Toast.Make(message, (ToastDuration)duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
