using CGPost.Models.Tracking;

namespace CGPost.Services.Services.Implementations
{
    public sealed class PayloadTransformerService : IPayloadTransformerService
    {
        private readonly IDateTimeToHumanReadableDateParserService _dateTimeHumanParser;

        public PayloadTransformerService(IDateTimeToHumanReadableDateParserService dateTimeHumanParser)
        {
            _dateTimeHumanParser = dateTimeHumanParser;
        }

        public TrackingData? TransformToTrackingData(DocumentElement? documentElement)
        {
            if (documentElement == null)
                return null;

            var newTrackingData = new TrackingData();
            var events = new List<TrackingDataPoint>();
            foreach (var tableRow in documentElement.TableName)
            {
                events.Add(new()
                {
                    TrackingNumber = tableRow.PrijemniBroj,
                    Date = tableRow.datum,
                    Description = tableRow.Opis,
                    PostIndex = tableRow.Posta,
                    EventType = tableRow.Naziv,
                    Order = tableRow.rowOrder,
                    Place = tableRow.mesto,
                    HumanReadableDate = _dateTimeHumanParser.Parse(tableRow.datum),
                });
            }

            foreach(var @event in events.OrderByDescending(x=>x.Date))
            {
                newTrackingData.TrackingEvents.Add(@event);
            }

            return newTrackingData;
        }
    }
}
