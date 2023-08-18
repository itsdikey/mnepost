using DKIMVVM;
using System.Collections.ObjectModel;

namespace CGPost.Models.Tracking
{
    public sealed class TrackingData : NotifierBase
    {
        private ObservableCollection<TrackingDataPoint> _trackingEvents;

        public TrackingData()
        {
            _trackingEvents = new ObservableCollection<TrackingDataPoint>();
        }
        public TrackingData(params TrackingDataPoint[] trackingDataEvents)
        {
            _trackingEvents = new ObservableCollection<TrackingDataPoint>(trackingDataEvents);
        }
        public TrackingData(IEnumerable<TrackingDataPoint> trackingDataEvents)
        {
            _trackingEvents = new ObservableCollection<TrackingDataPoint>(trackingDataEvents);
        }
        public ObservableCollection<TrackingDataPoint> TrackingEvents { get => _trackingEvents; set => SetProperty(ref _trackingEvents, value); }
    }

    public sealed class TrackingDataPoint : NotifierBase
    {
        private uint _postIndex;
        private string? _place;
        private string? _trackingNumber;
        private string? _description;
        private DateTime _date;
        private string? _eventType;
        private string? _humanReadableDate;
        private uint _order;
        private bool _isLast;

        public uint PostIndex { get => _postIndex; set => SetProperty(ref _postIndex, value); }
        public string? Place { get => _place; set => SetProperty(ref _place, value); }
        public string? TrackingNumber { get => _trackingNumber; set => SetProperty(ref _trackingNumber, value); }
        public string? Description { get => _description; set => SetProperty(ref _description, value); }
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(nameof(DateView)); OnPropertyChanged(nameof(Date)); } }
        public string? EventType { get => _eventType; set => SetProperty(ref _eventType, value); }
        public string? HumanReadableDate { get => _humanReadableDate; set { SetProperty(ref _humanReadableDate, value); OnPropertyChanged(nameof(DateView)); } }
        public string? DateView => string.IsNullOrEmpty(HumanReadableDate) ? Date.ToString() : HumanReadableDate;
        public uint Order { get => _order; set => SetProperty(ref _order, value); }
        public bool IsLast { get => _isLast; set => SetProperty(ref _isLast, value); }

        public static TrackingDataPoint Empty
        {
            get
            {
                return new TrackingDataPoint()
                {
                    HumanReadableDate = "N/A",
                    PostIndex = 00000,
                    Description = "Not available",
                    IsLast = true
                };
            }
        }
    }
}
