using System;
using System.Text;
using BioEnumerator.APIServer.APIObjs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;

namespace BioEnumerator.RemoteHelper.ExternalTool
{
    internal class RequestResponseHelper
    {
        public RequestException ReadRequestException(IRestResponse response)
        {
            var errorText = "Unexpected error occured. This normally happens when the Remote Server is not accessible either due to network or other related problems";
            var messageId = "";

            try
            {
                string msg;
                var requestError = DeserializeStream<RequestError>(response.Content, "requestError", out msg);

                if (requestError != null)
                {
                    if (requestError.PolicyException != null)
                    {
                        errorText = requestError.PolicyException.Text;
                        messageId = requestError.PolicyException.MessageId;
                    }
                    else if (requestError.ServiceException != null)
                    {
                        errorText = requestError.ServiceException.Text;
                        messageId = requestError.ServiceException.MessageId;
                    }
                }
                else
                {
                    errorText = msg;

                }
            }
            catch (Exception e)
            {
                return new RequestException(e, (int)response.StatusCode);
            }

            return new RequestException(errorText, messageId, (int)response.StatusCode);
        }

        public string GetAuthorizationHeader(string username, string password)
        {
            var credentials = username + ":" + password;
            var credentialsAsBytes = Encoding.UTF8.GetBytes(credentials);
            return Convert.ToBase64String(credentialsAsBytes).Trim();
        }
        public T Deserialize<T>(IRestResponse response, string rootElement, out string msg) where T : new()
        {
            var responseCode = (int)response.StatusCode;

            if (responseCode >= 200 && responseCode < 300)
            {
               try
                {
                   
                    return DeserializeStream<T>(response.Content, rootElement, out  msg);
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    return new T();
                }
            }

            var error = ReadRequestException(response);
            msg = error.Message;
            return new T();
        }

        public T DeserializeStream<T>(string content, string rootElement, out string msg) where T : new()
        {
            return ConvertJsonToObject<T>(content, rootElement, out  msg);
        }

        public T ConvertJsonToObject<T>(string json, out string msg) where T : new()
        {
            return ConvertJsonToObject<T>(json, null, out  msg);
        }

        public static T ConvertJsonToObject<T>(string json, string rootElement, out string msg) where T : new()
        {
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTime,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            msg = "";
            try
            {
                if (null == rootElement) return JsonConvert.DeserializeObject<T>(json, settings);
                var rootObject = JObject.Parse(json);
                var rootToken = rootObject.SelectToken(rootElement);

                return JsonConvert.DeserializeObject<T>(rootToken.ToString(), settings);
            }
            catch (Exception e)
            {
                msg = e.Message;
                return new T();
            }
        }
    }
}
