using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public interface IHttpClient
    {
        // this is to limit the API calls to GET,POST,PUT,DELETE

        Task<string> GetStringAsync(string uri, string autherizationToken = null,
            string autherizationMethod = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item,
            string autherizationToken = null,
            string autherizationMethod = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item,
            string autherizationToken = null,
            string autherizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string autherizationToken = null,
            string autherizationMethod = "Bearer");
    }
}
