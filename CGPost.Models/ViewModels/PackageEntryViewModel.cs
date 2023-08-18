using DKIMVVM;

namespace CGPost.Models.ViewModels
{
    public sealed class PackageEntryViewModel : NotifierBase
    {
        private int _id;
        private string? _name;
        private string? _trackingNumber;
        private string? _lastTrackingInfo;

        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        public string? TrackingNumber { get => _trackingNumber; set => SetProperty(ref _trackingNumber, value); }
        public string? LastTrackingInfo { get => _lastTrackingInfo; set => SetProperty(ref _lastTrackingInfo, value); }
        public int Id { get => _id; set => SetProperty(ref _id, value); }
    }
}
