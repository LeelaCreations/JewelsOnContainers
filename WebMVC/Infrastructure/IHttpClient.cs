﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public interface IHttpClient
    {
        // this is to limit the API calls to GET,POST,PUT,DELETE

        Task<string> GetStringAsync(string uri, string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");
    }
}
