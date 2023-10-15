using CloudFlareDns.Objects.Universal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CloudFlareDns
{
    public class Core
    {
        /// <summary>
        /// Specifies the number of items or entries to display or process on each page or iteration
        /// </summary>
        public static long PerPage = 100;

        /// <summary>
        /// Represents the server or endpoint for an API (Application Programming Interface) that provides access to specific functionalities or data
        /// </summary>
        private const string ApiServer = "https://api.cloudflare.com/client/v4";

        /// <summary>
        /// Sends an HTTP GET request to retrieve data from a remote server
        /// </summary>
        /// <param name="xAuthKey"></param>
        /// <param name="xAuthEmail"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SendGetRequest(string xAuthKey, string xAuthEmail, string url)
        {
            HttpResponseMessage httpResponseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("GET"), $"{ApiServer}{url}"))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Key", $"{xAuthKey}");
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Email", $"{xAuthEmail}");
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            // json
            string json = await httpResponseMessage.Content.ReadAsStringAsync();

            // No OK
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                JObject result = JObject.Parse(json);
                List<Error> error = JsonConvert.DeserializeObject<List<Error>>($"{result["errors"]}") ?? new List<Error>();

                // We validate the error type, and if the error number is 81044, it indicates that the record doesn't exist.
                if (error.Exists(x => x.Code == 81044) == false)
                {
                    // Therefore, we can mark it as error-free to return an empty result instead of an error.
                    json = "{}";
                    return json;
                }

                // It's a error
                throw new Exception($"{error[0].Code} - {error[0].Message}");
            }

            return json;
        }

        /// <summary>
        /// Sends an HTTP POST request to submit data to a remote server
        /// </summary>
        /// <param name="xAuthKey"></param>
        /// <param name="xAuthEmail"></param>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SendPostRequest(string xAuthKey, string xAuthEmail, string url, string content)
        {
            HttpResponseMessage httpResponseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("POST"), $"{ApiServer}{url}"))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Key", $"{xAuthKey}");
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Email", $"{xAuthEmail}");
                    httpRequestMessage.Content = new StringContent(content);
                    httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            // IsNullOrEmpty
            string json = await httpResponseMessage.Content.ReadAsStringAsync();

            // Empty
            if (string.IsNullOrEmpty(json) && httpResponseMessage.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception("there has been some error, the API has responded empty.");
            }

            // No Created
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Accepted:
                    break;

                default:
                    JObject result = JObject.Parse(json);
                    List<Error> error = JsonConvert.DeserializeObject<List<Error>>($"{result["errors"]}") ?? new List<Error>();
                    throw new Exception($"{error[0].Code} - {error[0].Message}");
                    break;
            }

            return json;
        }

        /// <summary>
        /// Sends an HTTP PUT request to update data on a remote server
        /// </summary>
        /// <param name="xAuthKey"></param>
        /// <param name="xAuthEmail"></param>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SendPutRequest(string xAuthKey, string xAuthEmail, string url, string content)
        {
            HttpResponseMessage httpResponseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("PUT"), $"{ApiServer}{url}"))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Key", $"{xAuthKey}");
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Email", $"{xAuthEmail}");
                    httpRequestMessage.Content = new StringContent(content);
                    httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            // IsNullOrEmpty
            string json = await httpResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("there has been some error, the API has responded empty.");
            }

            // No Update
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                JObject result = JObject.Parse(json);
                List<Error> error = JsonConvert.DeserializeObject<List<Error>>($"{result["errors"]}") ?? new List<Error>();
                throw new Exception($"{error[0].Code} - {error[0].Message}");
            }

            return json;
        }

        /// <summary>
        /// Sends an HTTP PATCH request to partially modify data on a remote server
        /// </summary>
        /// <param name="xAuthKey"></param>
        /// <param name="xAuthEmail"></param>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SendPatchRequest(string xAuthKey, string xAuthEmail, string url, string content)
        {
            HttpResponseMessage httpResponseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), $"{ApiServer}{url}"))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Key", $"{xAuthKey}");
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Email", $"{xAuthEmail}");
                    httpRequestMessage.Content = new StringContent(content);
                    httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            // IsNullOrEmpty
            string json = await httpResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("there has been some error, the API has responded empty.");
            }

            // No Update
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                JObject result = JObject.Parse(json);
                List<Error> error = JsonConvert.DeserializeObject<List<Error>>($"{result["errors"]}") ?? new List<Error>();
                throw new Exception($"{error[0].Code} - {error[0].Message}");
            }

            return json;
        }

        /// <summary>
        /// Sends an HTTP DELETE request to remove data from a remote server
        /// </summary>
        /// <param name="xAuthKey"></param>
        /// <param name="xAuthEmail"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task SendDeleteRequest(string xAuthKey, string xAuthEmail, string url)
        {
            HttpResponseMessage httpResponseMessage;
            using (var httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("DELETE"), $"{ApiServer}{url}"))
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Key", $"{xAuthKey}");
                    httpRequestMessage.Headers.TryAddWithoutValidation("X-Auth-Email", $"{xAuthEmail}");
                    httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                }
            }

            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.OK:
                    break;

                default:
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    JObject result = JObject.Parse(json);
                    List<Error> error = JsonConvert.DeserializeObject<List<Error>>($"{result["errors"]}") ?? new List<Error>();
                    throw new Exception($"{error[0].Code} - {error[0].Message}");
            }
        }
    }
}