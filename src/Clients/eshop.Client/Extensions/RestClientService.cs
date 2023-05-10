using eshop.Client.Dtos;
using RestSharp;

namespace eshop.Client.Extensions
{
    public class RestClientService
    {
        public RestClient RestClient { get; set; }
        public RestClientService()
        {
            RestClient = new RestClient();
        }

        public async Task<RestResponse?> SendRequestAsync(string url
            , Method method
            , Object body = null
            , Dictionary<string, string> parameters = null)
        {
            try
            {
                var uriBuilder = new UriBuilder(APIEndpoints.GatewayUrl);
                uriBuilder.Path = Path.Combine(uriBuilder.Path, url);
                var request = new RestRequest(uriBuilder.Uri, method);

                if (body != null)
                    request.AddJsonBody(body);

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        request.AddParameter(parameter.Key, parameter.Value);
                    }
                }

                return await RestClient.ExecuteAsync(request);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
