using Newtonsoft.Json;
using QuizletClone.API.Model;
using QuizletClone.API.Payload;
using QuizletClone.API.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizletClone.API.Services
{
    public class SetService
    {
        private readonly QuizletCloneHTTPClientFactory _httpClientFactory;

        public SetService(QuizletCloneHTTPClientFactory quizletCloneHTTPClientFactory)
        {
            _httpClientFactory = quizletCloneHTTPClientFactory;
        }

        public async Task<List<SetPresenter>> GetListSet(string? keyword) 
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/set";

                if (!string.IsNullOrEmpty(keyword))
                {
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["keyword"] = keyword;

                    uri += "?" + query;
                }

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var sets = JsonConvert.DeserializeObject<List<SetPresenter>>(jsonResponse);

                return sets;
            }
        }

        public async Task<List<SetPresenter>> GetMySets(string? keyword)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/set/my";

                if (!string.IsNullOrEmpty(keyword))
                {
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["keyword"] = keyword;

                    uri += "?" + query;
                }

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var sets = JsonConvert.DeserializeObject<List<SetPresenter>>(jsonResponse);

                return sets;
            }
        }

        public async Task<SetDetailPresenter> GetDetailSet(int setId)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{setId}";

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var set = JsonConvert.DeserializeObject<SetDetailPresenter>(jsonResponse);

                return set;
            }
        }

        public async Task<SetDetailPresenter> Create(SetPayload payload)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/set";

                var json = JsonConvert.SerializeObject(payload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(uri, data);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var set = JsonConvert.DeserializeObject<SetDetailPresenter>(jsonResponse);

                return set;
            }
        }

        public async Task Update(int id, SetPayload payload)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{id}";

                var json = JsonConvert.SerializeObject(payload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PutAsync(uri, data);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{id}";

                HttpResponseMessage responseMessage = await client.DeleteAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public async Task<TermPresenter> GetRandomLearningTerm(int setId)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{setId}/learning/term";

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var term = JsonConvert.DeserializeObject<TermPresenter>(jsonResponse);

                return term;
            }
        }

        public async Task<CountLearningTermPresenter> CountLearningTermProgress(int setId)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{setId}/learning/term/progress";

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var term = JsonConvert.DeserializeObject<CountLearningTermPresenter>(jsonResponse);

                return term;
            }
        }

        public async Task ReportLearningProgress(int setId, ReportTermPayload payload)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"api/set/{setId}/learning/report";

                var json = JsonConvert.SerializeObject(payload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(uri, data);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
