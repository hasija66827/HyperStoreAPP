using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace SDKTemplate
{
    public partial class Utility
    {
        public static async Task<T> RetrieveAsync<T>(string baseURI, string queryString, object content)
        {
            string httpResponseBody = "";
            string actionURI = "";
            if (queryString != null)
                actionURI = baseURI + "/" + queryString;
            else
                actionURI = baseURI;

            try
            {
                var response = await Utility.HttpGet(actionURI, content);
                if (response.StatusCode != HttpStatusCode.Ok)
                    throw new Exception(response.Content.ToString());
                httpResponseBody = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<T>(httpResponseBody);
                return results;
            }
            catch (Exception ex)
            {
                var logMessage = "Error: " + ex.HResult + " Message: " + ex.Message;
                var userMessage = ex.Message;

                if (ex.HResult == -2147012867 || ex.HResult== -2147012889)
                    userMessage = "Could not connect to server. Please check the internet connection.";

                ErrorNotification.PopUpHTTPGetErrorNotifcation(ExtractAPIName(actionURI), userMessage);
                return default(T);
            }
        }

        private static async Task<HttpResponseMessage> HttpGet(string actionURI, object content)
        {
            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            HttpClient httpClient = new HttpClient(httpBaseProtocolFilter);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(actionURI));

            if (content != null)
                request.Content = new HttpStringContent(JsonConvert.SerializeObject(content), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<T> CreateAsync<T>(string baseURI, object content)
        {
            try
            {
                var serializeContent = JsonConvert.SerializeObject(content);
                var response = await Utility.HttpPost(baseURI, serializeContent);
                if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.Ok)
                    throw new Exception(response.Content.ToString());
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(httpResponseBody);
                return result;
            }
            catch (Exception ex)
            {
                var userMessage = ex.Message;
                if (ex.HResult == -2147012867 || ex.HResult == -2147012889)
                    userMessage = "Could not connect to server. Please check the internet connection.";

                ErrorNotification.PopUpHTTPPostErrorNotifcation(ExtractAPIName(baseURI), userMessage);
                //TODO: handle different types of exception
                return default(T);
            }
        }

        private static async Task<HttpResponseMessage> HttpPost(string actionURI, string content)
        {
            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            HttpClient httpClient = new HttpClient(httpBaseProtocolFilter);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(actionURI));
            request.Content = new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<T> UpdateAsync<T>(string baseURI, string queryString, object content)
        {
            string actionURI = "";
            if (queryString != null)
                actionURI = baseURI + "/" + queryString;
            else
                actionURI = baseURI;
            try
            {
                var serializeContent = JsonConvert.SerializeObject(content);
                var response = await Utility.HttpPut(actionURI, serializeContent);
                if (response.StatusCode != HttpStatusCode.Ok)
                    throw new Exception(response.Content.ToString());
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(httpResponseBody);
                return result;
            }
            catch (Exception ex)
            {
                var userMessage = ex.Message;
                if (ex.HResult == -2147012867 || ex.HResult == -2147012889)
                    userMessage = "Could not connect to server. Please check the internet connection.";

                ErrorNotification.PopUpHTTPPutErrorNotifcation(ExtractAPIName(actionURI), userMessage);
                //TODO: handle different types of exception
                return default(T);
            }
        }

        private static async Task<HttpResponseMessage> HttpPut(string actionURI, string content)
        {
            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            HttpClient httpClient = new HttpClient(httpBaseProtocolFilter);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri(actionURI));
            request.Content = new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            try
            {
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ExtractAPIName(string BaseURI)
        {
            //TODO: what if the we have a guid at the last not the apiname, as we have in post request.
            var startIndexOfAPI = BaseURI.LastIndexOf("/") + 1;
            return BaseURI.Substring(startIndexOfAPI);
        }
    }
}
