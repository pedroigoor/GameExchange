using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace WebApi.Test
{
    public class GameExchangeClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public GameExchangeClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "pt-BR")
        {
            ChangeRequestCulture(culture);
            //AuthorizeRequest(token);

            return await _httpClient.PostAsJsonAsync(method, request);
        }

        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }
    }

}