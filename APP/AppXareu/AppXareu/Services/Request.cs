using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppXareu.Models;
using Newtonsoft.Json;

namespace AppXareu.Services
{
    public class Request
    {
        public async Task<int> PostReturnIntAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }


        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = data;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            else
                throw new Exception(serialized);

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(data)); 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 
            HttpResponseMessage response = await httpClient.PutAsync(uri, content); 
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = data;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            else
                throw new Exception(serialized);

            return result;
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(serialized);
            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized)); return result;
        }

        public async Task<string> DeleteAsync<TResult>(string uri, Usuario data, string token)
        {
            // 1. Crie o HttpClient. É recomendado reutilizar instâncias de HttpClient
            // em vez de criar uma nova para cada requisição.
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 2. Crie a requisição (HttpRequestMessage) com o método DELETE e a URI.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);

            // 3. Serialize seu objeto 'data' para JSON e adicione-o como conteúdo da requisição.
            string jsonData = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // 4. Envie a requisição manualmente construída usando SendAsync.
            HttpResponseMessage response = await httpClient.SendAsync(request);

            System.Diagnostics.Debug.WriteLine($"Essa é a URL: {uri}");
            string serialized = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return serialized;
            else
                throw new Exception(serialized);
        }

    }
}
