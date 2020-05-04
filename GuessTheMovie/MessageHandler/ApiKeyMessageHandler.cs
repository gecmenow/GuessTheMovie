using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GuessTheMovie.MessageHandler
{
    public class ApiKeyMessageHandler: DelegatingHandler
    {
        const string ApiKeyToCheck = "ee542824-4140-4f1e-8aa7-384d1602c256";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool valdidKey = false;

            IEnumerable<string> requestHeaders;

            var checkAPiKeyExist = httpRequestMessage.Headers.TryGetValues("API-KEY", out requestHeaders);

            if (checkAPiKeyExist)
            {
                if(requestHeaders.FirstOrDefault().Equals(ApiKeyToCheck))
                {
                    valdidKey = true;
                }
            }

            if(!valdidKey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid ApiKey");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);

            return response;
        }
    }
}