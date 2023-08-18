using System.Xml.Serialization;
using System.Xml;
using CGPost.Models.Tracking;

namespace CGPost.Services.Services.Implementations
{
    public sealed class TrackingService : ITrackingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPayloadTransformerService _payloadTransformerService;
        private readonly string _apiBase;
        public TrackingService(IHttpClientFactory httpClientFactory, IPayloadTransformerService payloadTransformerService)
        {
            _httpClientFactory = httpClientFactory;
            _apiBase = "https://e-racuni.postacg.me/TTService/Service1.asmx";
            _payloadTransformerService = payloadTransformerService;
        }

        public async Task<TrackingData?> GetTrackingInfo(string trackingNumber)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync(_apiBase, GenerateBody(trackingNumber));

                XmlTextReader textReader = new XmlTextReader(response.Content.ReadAsStream());
                textReader.ReadToDescendant("DocumentElement");

                XmlSerializer serializer = new XmlSerializer(typeof(DocumentElement));
                var documentElement = serializer.Deserialize(textReader.ReadSubtree());
                textReader.Close();

                if (documentElement != null)
                {
                    return _payloadTransformerService.TransformToTrackingData(documentElement as DocumentElement);
                }
            }
            catch
            {
                return null;
            }
       
            return null;
        }

        private StringContent GenerateBody(string trackingNumber)
        {
            var body = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><InformacijaOPosiljci xmlns=\"http://TrackTrace.com\"><strPrijemniBroj>{0}</strPrijemniBroj></InformacijaOPosiljci></soap:Body></soap:Envelope>";
            return new StringContent(string.Format(body, trackingNumber), null, "text/xml");
        } 
    }
}
