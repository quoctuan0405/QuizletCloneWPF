using Newtonsoft.Json;
using QuizletClone.API.Model;
using QuizletClone.API.Payload;
using QuizletClone.API.Presenter;
using QuizletClone.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.API.Services
{
    public class UserService
    {
        private readonly QuizletCloneHTTPClientFactory _httpClientFactory;

        public UserService(QuizletCloneHTTPClientFactory quizletCloneHTTPClientFactory)
        {
            _httpClientFactory = quizletCloneHTTPClientFactory;
        }

        public async Task<TokenPresenter> Login(LoginPayload payload)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/auth/login";

                var json = JsonConvert.SerializeObject(payload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(uri, data);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                TokenPresenter response = JsonConvert.DeserializeObject<TokenPresenter>(jsonResponse);

                if (!string.IsNullOrEmpty(response.Token))
                {
                    _httpClientFactory.SetToken(response.Token);
                }

                return response;
            }
        }

        public async Task<TokenPresenter> Signup(SignupPayload payload)
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/auth/signup";

                var json = JsonConvert.SerializeObject(payload);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(uri, data);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                try
                {
                    TokenPresenter response = JsonConvert.DeserializeObject<TokenPresenter>(jsonResponse);

                    if (!string.IsNullOrEmpty(response.Token))
                    {
                        _httpClientFactory.SetToken(response.Token);
                    }

                    return response;

                } catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<UserPresenter> GetMe()
        {
            using (HttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = "api/auth/me";

                HttpResponseMessage responseMessage = await client.GetAsync(uri);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                UserPresenter response = JsonConvert.DeserializeObject<UserPresenter>(jsonResponse);

                return response;
            }
        }
    }
}
