using Commons.Consts;
using Commons.ExceptionHandling.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Commons.HttpClientRequests {
    public class HttpClientService {

        private HttpClient _client;

        public HttpClientService(HttpClient client) {
            this._client = client;
        }

        public async Task<T> SendRequest<T>(HttpMethod method, string url, string authorizationToken, object body = null) {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            request.Headers.Add(HttpClientConsts.HEADER_ACCEPT, "application/json");
            request.Headers.Add(HttpClientConsts.HEADER_AUTHORIZATION, authorizationToken);


            if ((method.Equals(HttpMethod.Post) || method.Equals(HttpMethod.Put)) && body != null) {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content)) {
                    return default(T);
                }

                return JsonConvert.DeserializeObject<T>(content);
            }

            throw new HttpClientException($"There was an error while communicating with other microservices! Code: ${response.StatusCode} | Message: {response.ReasonPhrase}", "Generic Http Client");

        }

        public async void SendEmail(List<string> recipients, string title, string content) {

            string json = JsonConvert.SerializeObject(new {
                title,
                content,
                recipients
            });

            await _client.PostAsync("http://localhost:40009/api/mails/send", new StringContent(json, Encoding.UTF8, "application/json"));
        }

    }
}
