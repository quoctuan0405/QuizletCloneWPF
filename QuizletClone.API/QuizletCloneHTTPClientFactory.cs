using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.API
{
    public class QuizletCloneHTTPClientFactory
    {
        private string? _token;

        public QuizletCloneHTTPClientFactory()
        {

        }

        public QuizletCloneHTTPClient CreateHttpClient()
        {
            return new QuizletCloneHTTPClient(_token);
        }

        public void SetToken(string token)
        {
            _token = token ?? string.Empty;
        }
    }
}
