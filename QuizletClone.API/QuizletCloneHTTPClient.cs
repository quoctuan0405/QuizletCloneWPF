using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.API
{
    public class QuizletCloneHTTPClient : HttpClient
    {
        public QuizletCloneHTTPClient(string? token)
        {
            this.BaseAddress = new Uri("https://localhost:7248/");
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T> PostAsync<T>(string uri, HttpContent payload)
        {
            HttpResponseMessage response = await PostAsync(uri, payload);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
