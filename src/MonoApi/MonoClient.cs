using MonoApi.Exceptions;
using MonoApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonoApi
{
    public class MonoClient
    {
        private HttpClient _client { get; set; }
        private string _domain { get; set; } = "https://api.monobank.ua/";

        public event StatusCodeCallback CallbackUnauthorized;
        public event StatusCodeCallback CallbackForbidden;
        public event StatusCodeCallback CallbackBadRequest;
        public event StatusCodeCallback CallbackInternalServerError;

        public MonoClient(string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            this._client = new HttpClient();
            this._client.BaseAddress = new Uri(this._domain);

            this._client.DefaultRequestHeaders.Add("X-Token", token);
        }

        public List<Currency> GetCurrency() =>
            this.Execute<List<Currency>>(HttpMethod.Get, "bank/currency");

        public async Task<List<Currency>> GetCurrencyAsync() =>
            await this.ExecuteAsync<List<Currency>>(HttpMethod.Get, "bank/currency");

        public Client GetClientInfo() =>
            this.Execute<Client>(HttpMethod.Get, "personal/client-info");

        public async Task<Client> GetClientInfoAsync() =>
            await this.ExecuteAsync<Client>(HttpMethod.Get, "personal/client-info");

        public List<Extract> GetExtract(string accountId, DateTime from, DateTime? to = null) =>
            this.Execute<List<Extract>>(HttpMethod.Get, $"personal/statement/{accountId}/{this.GetUnix(from)}/{this.GetUnix(to)}");

        public async Task<List<Extract>> GetExtractAsync(string accountId, DateTime from, DateTime? to = null) =>
            await this.ExecuteAsync<List<Extract>>(HttpMethod.Get, $"personal/statement/{accountId}/{this.GetUnix(from)}/{this.GetUnix(to)}");

        public void SetWebHook(string webHookUrl) =>
            this.Execute(HttpMethod.Post, "personal/webhook", new SetWebHook(webHookUrl));

        public async Task SetWebHookAsync(string webHookUrl) =>
            await this.ExecuteAsync(HttpMethod.Post, "personal/webhook", new SetWebHook(webHookUrl));


        #region ---Execute---
        private T Execute<T>(HttpMethod method, string url, object body = null) =>
            this.ExecuteAsync<T>(method, url, body).ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task<T> ExecuteAsync<T>(HttpMethod method, string url, object body = null)
        {
            using (var response = await this.ExecuteAsync(method, url, body))
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        private HttpResponseMessage Execute(HttpMethod method, string url, object body = null) =>
            this.ExecuteAsync(method, url, body).ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task<HttpResponseMessage> ExecuteAsync(HttpMethod method, string url, object body = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                if (body != null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    request.Content = content;
                }

                var response = await this._client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var callback = this.GetCallback(response.StatusCode);
                    if (callback != null) callback(content);
                    throw new RequestException(response.StatusCode, content);
                }
            }
        }
        #endregion

        private StatusCodeCallback GetCallback(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Unauthorized: return this.CallbackUnauthorized;
                case HttpStatusCode.Forbidden: return this.CallbackForbidden;
                case HttpStatusCode.BadRequest: return this.CallbackBadRequest;
                case HttpStatusCode.InternalServerError: return this.CallbackInternalServerError;
                default:
                    return null;
            }
        }

        private string GetUnix(DateTime? time)
        {
            if (time == null) return string.Empty;

            return new DateTimeOffset(time.Value).ToUnixTimeSeconds().ToString();
        }

        public delegate void StatusCodeCallback(string message);

    }
}
