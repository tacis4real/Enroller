using System;
using System.Text;
using Newtonsoft.Json;

namespace BioEnumerator.RemoteHelper.ExternalTool
{
  
    [Serializable]
    public class RequestError
    {
        public const int SERVICEEXCEPTION = 1;
        public const int POLICYEXCEPTION = 2;
        private ServiceException serviceException;
        private PolicyException policyException;
        [JsonProperty(PropertyName = "clientCorrelator")]
        public string ClientCorrelator;
        [JsonIgnore]
        public int ExceptionType;
        [JsonIgnore]
        public int HttpResponseCode;

        [JsonProperty(PropertyName = "serviceException")]
        public ServiceException ServiceException
        {
            get
            {
                return serviceException;
            }
            set
            {
                ExceptionType = 1;
                serviceException = value;
            }
        }

        [JsonProperty(PropertyName = "policyException")]
        public PolicyException PolicyException
        {
            get
            {
                return policyException;
            }
            set
            {
                policyException = value;
                ExceptionType = 2;
            }
        }

        public RequestError(int type, int httpResponseCode, string messageId, string clientCorrelator, string text, params string[] variables)
        {
            if (type == 1)
            {
                serviceException = new ServiceException
                {
                    MessageId = messageId, 
                    Text = text, 
                    Variables = variables
                };
            }
            else if (type == 2)
            {
                policyException = new PolicyException
                {
                    MessageId = messageId, 
                    Text = text, 
                    Variables = variables
                };
            }
            ExceptionType = type;
            ClientCorrelator = clientCorrelator;
            HttpResponseCode = httpResponseCode;
        }

        public RequestError()
        {
            HttpResponseCode = 400;
            ExceptionType = 0;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (ClientCorrelator != null)
            {
                stringBuilder.Append("clientCorrelator=");
                stringBuilder.Append(ClientCorrelator);
            }
            if (serviceException != null)
            {
                stringBuilder.Append("serviceException = {");
                stringBuilder.Append("messageId = ");
                stringBuilder.Append(serviceException.MessageId);
                stringBuilder.Append(", text = ");
                stringBuilder.Append(serviceException.Text);
                stringBuilder.Append(", variables = ");
                if (serviceException.Variables != null)
                {
                    stringBuilder.Append("{");
                    for (int index = 0; index < serviceException.Variables.Length; ++index)
                    {
                        stringBuilder.Append("[");
                        stringBuilder.Append(index);
                        stringBuilder.Append("] = ");
                        stringBuilder.Append(serviceException.Variables[index]);
                    }
                    stringBuilder.Append("}");
                }
                stringBuilder.Append("}");
            }
            if (policyException != null)
            {
                stringBuilder.Append("policyException = {");
                stringBuilder.Append("messageId = ");
                stringBuilder.Append(policyException.MessageId);
                stringBuilder.Append(", text = ");
                stringBuilder.Append(policyException.Text);
                stringBuilder.Append(", variables = ");
                if (policyException.Variables != null)
                {
                    stringBuilder.Append("{");
                    for (int index = 0; index < policyException.Variables.Length; ++index)
                    {
                    stringBuilder.Append("[");
                    stringBuilder.Append(index);
                    stringBuilder.Append("] = ");
                    stringBuilder.Append(policyException.Variables[index]);
                    }
                    stringBuilder.Append("}");
                }
                stringBuilder.Append("}");
            }
            return stringBuilder.ToString();
        }
    }
}
