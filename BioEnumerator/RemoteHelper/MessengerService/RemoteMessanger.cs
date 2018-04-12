using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.RemoteHelper.ExternalTool;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.RemoteHelper.MessengerService
{

    internal class RemoteMessanger
    {

        private readonly RestClient _client;
        private readonly RestRequest _request;


        public RemoteMessanger(RemoteProcessType processType, UploadStationInfo authParam, RemoteRequestType requestType = RemoteRequestType.Registration, Method method = Method.POST)
        {
            try
            {
                if (authParam == null || authParam.StationInfoId < 1 || string.IsNullOrEmpty(authParam.APIAccessKey))
                {
                    throw new ApplicationException("Empty / Invalid Remote Server Authentication Parameters");
                }
                var servAdd = authParam.HostServerAddress;
                if (string.IsNullOrEmpty(servAdd) || servAdd.Length < 5)
                {
                    throw new ApplicationException("Remote Server Address Not Configured");
                }
                _client = new RestClient(servAdd);
                switch (requestType)
                {
                    case RemoteRequestType.Registration:
                        _request = new RestRequest("bioEnroll/DataKiosk/" + processType, method)
                        {
                            JsonSerializer = new JsonSerializer(),
                            RequestFormat = DataFormat.Json
                        };
                        break;
                    case RemoteRequestType.Report:
                        _request = new RestRequest("Messaging/LRServiceCloud/" + processType, method)
                        {
                            JsonSerializer = new JsonSerializer(),
                            RequestFormat = DataFormat.Json
                        };
                        break;
                }

                _request.AddHeader("content-type", "application/json");
                _request.AddHeader("APIAccessKey", string.Format("{0}", authParam.APIAccessKey));
                _request.AddHeader("StationName", string.Format("{0}", authParam.StationName));
                _request.AddHeader("StationId", string.Format("{0}", authParam.StationKey));
                _request.AddHeader("EnrollerRegId", string.Format("{0}", authParam.EnrollerRegId));
                _request.AddHeader("ApiVersion", string.Format("{0}", 1));
                // _request.Timeout = 180000;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Remote Server Address Not Configured");
            }


        }
        public RemoteMessanger(RemoteProcessType processType, string serverAddress, Method method = Method.POST)
        {
            try
            {
                if (string.IsNullOrEmpty(serverAddress) || serverAddress.Length < 5)
                {
                    throw new ApplicationException("Remote Server Address Not Configured");
                }

                _client = new RestClient(serverAddress);
                _request = new RestRequest("bioEnroll/DataKiosk/" + processType, method)
                {
                    JsonSerializer = new JsonSerializer(),
                    RequestFormat = DataFormat.Json
                };
                _request.AddHeader("content-type", "application/json");

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Remote Server Address Not Configured");
            }

        }
        public RemoteMessanger(string serverAddress, Method method = Method.POST)
        {
            try
            {
                if (string.IsNullOrEmpty(serverAddress) || serverAddress.Length < 5)
                {
                    throw new ApplicationException("Remote Server Address Not Configured");
                }

                _client = new RestClient(serverAddress);
                //bioEnroll/DataKiosk/AuthorizeStationAccess
                //bioEnumerator/DataKiosk/AuthorizeStationAccess
                _request = new RestRequest("bioEnroll/DataKiosk/AuthorizeStationAccess", method)
                {
                    JsonSerializer = new JsonSerializer(),
                    RequestFormat = DataFormat.Json
                };
                _request.AddHeader("content-type", "application/json");

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Remote Server Address Not Configured");
            }

        }
        
        
        
        
        #region API Methods

        public BulkBeneficiaryRegResponseObj ProcessBulkData(BulkBeneficiaryRegObj bulkItems, out string msg)
        {

            try
            {
                _request.AddBody(bulkItems);
                var response = _client.Execute(_request);
                var responseCode = (int)response.StatusCode;
                if (responseCode >= 200 && responseCode < 300)
                {
                    var deserializedResponse = new RequestResponseHelper().Deserialize<BulkBeneficiaryRegResponseObj>(response, null, out msg);
                    return deserializedResponse;
                }

                var exception = new RequestResponseHelper().ReadRequestException(response);
                msg = exception == null ? "Unknown Exception Occurred!" : exception.Message;
                return null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = "Error: " + ex.Message;
                return null;
            }

        }

        public StationRespObj AuthourizeAccess(AccessParameter authParameter, out string msg)
        {

            try
            {
                _request.AddBody(authParameter);
                var response = _client.Execute(_request);
                var responseCode = (int)response.StatusCode;
                if (responseCode >= 200 && responseCode < 300)
                {

                    var deserializedResponse = new RequestResponseHelper().Deserialize<StationRespObj>(response, null, out msg);
                    return deserializedResponse;
                }

                var exception = new RequestResponseHelper().ReadRequestException(response);
                msg = exception == null ? "Unknown Exception Occurred!" : exception.Message;
                return null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = "Error: " + ex.Message;
                return null;
            }

        }


        public StationRespObj RemoteLogin(RemoteLoginParameter loginParameter, out string msg)
        {

            try
            {
                _request.AddBody(loginParameter);
                var response = _client.Execute(_request);
                var responseCode = (int)response.StatusCode;
                if (responseCode >= 200 && responseCode < 300)
                {

                    var deserializedResponse = new RequestResponseHelper().Deserialize<StationRespObj>(response, null, out msg);
                    return deserializedResponse;
                }

                var exception = new RequestResponseHelper().ReadRequestException(response);
                msg = exception == null ? "Unknown Exception Occurred!" : exception.Message;
                return null;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = "Error: " + ex.Message;
                return null;
            }

        }

        #endregion
        



    }

}
