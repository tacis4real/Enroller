using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioEnumerator.RemoteHelper.MessengerService
{


    public enum ProcessingStatus
    {
        Completed = 0,
        PartialCompletion = 1,
        OperationTerminated = -1,
    }

    public enum RequestChannel
    {

        SIM = 1,
        Mobile,
        Web,
        Support,
    }

    public enum VASType
    {
        Security_Service = 1,
        Balance_Checker,
        I_Share_Service,
        Account_Swap,
    }
    public enum RequestServiceType
    {
        Undefined = 0,
        SMS_Result_Notification = 1,
        Mobile_App_Result_Notification,
        Email_Result_Notification,
        EChart_Service,
        Advert_Service,
        Value_Added_Service,
        LRGist,
    }

    public enum RemoteRequestType
    {
        Registration = 1,
        Report,
        Access
    }
    public enum RemoteProcessType
    {
        DownloadStationUsers = 1,
        BulkBeneficiaryRegistration,
        GetBusinessLocations,
        GetBusinessLocationTypes,
        NewBusinessRegistration,
        GetBusinessTypes,
        BulkBusinessRegistration,
        GetLocalAreas,
        RemoteLogin,
        AuthorizeStationAccess,
        GetTaxCategories,
        UpdateBusinessRegistration,
        GetBusinessInfoReport,
        DownloadBusinessTRN,
        GetDataValidationSettings,
        GetBusinessCategories
    }

    //public class ResponseStatus
    //{
    //    public bool IsSuccessful;
    //    public ResponseMessage Message;
    //    public long ReturnedId;
    //    public string ReturnedValue;
    //}

    //public class ResponseMessage
    //{
    //    public string FriendlyMessage;
    //    public string TechnicalMessage;
    //    public string MessageCode;
    //    public int MessageId;
    //}

}
