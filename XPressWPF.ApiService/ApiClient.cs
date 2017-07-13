using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XPressWPF.ApiService
{
    // TODO: Refactor class to make it testable
    sealed class ApiClient
    {
        public HttpClient Client { get; set; }
        public ApiClient()
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:17693/api/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
