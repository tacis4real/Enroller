using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Repository;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Service
{
    internal class BeneficiaryService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly BeneficiaryRepository _beneficiaryRepository;

        public BeneficiaryService()
        {
            _beneficiaryRepository = new BeneficiaryRepository();
        }



        #region CRUD

        public Status AddBeneciary(BeneficiaryRegObj beneficiary)
        {
            try
            {
                return _beneficiaryRepository.AddBeneficiary(beneficiary);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable to add new role record";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }

        public bool UpdateBeneficiary(Beneficiary beneficiary, out string msg)
        {
            try
            {
                return _beneficiaryRepository.UpdateBeneficiary(beneficiary, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }


        public Beneficiary GetBeneficiary(long id)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiary(id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new Beneficiary();
            }
        }

        public BeneficiaryObj GetBeneficiaryObj(long id)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiaryObj(id);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new BeneficiaryObj();
            }
        }

        public List<BeneficiaryObj> GetBeneficiarys()
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiarys();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }

        public List<BeneficiaryObj> GetBusinessInfoReportDetail(string surname, string mobileNo)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiaryReportDetail(surname, mobileNo);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }

        public List<BeneficiaryObj> GetBusinessInfoReportDetail(string startDate, string stopDate, string surname, string mobileNo, int sexId, int stateId, int localAreaId)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiaryReportDetail(startDate, stopDate, surname, mobileNo, sexId, stateId, localAreaId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }

        public List<BeneficiaryObj> GetBusinessInfoReportDetail(string surname, string mobileNo, int stateId, int localAreaId, int status)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiaryReportDetail(surname, mobileNo, stateId, localAreaId, status);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }

        #region Upload Data

        public BulkBeneficiaryRegResponseObj UploadBulkData(BulkBeneficiaryRegObj bulkItems, UploadStationInfo stationInfo)
        {
            var response = new BulkBeneficiaryRegResponseObj
            {
                MainStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage()
                }
            };
            try
            {
                return _beneficiaryRepository.UploadBulkData(bulkItems, stationInfo);
            }
            catch (Exception ex)
            {
                response.MainStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.MainStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }



        #endregion
        

        public DashboardDataCount GetDashboardDataCount()
        {
            try
            {
                return _beneficiaryRepository.GetDashboardDataCount();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new DashboardDataCount();
            }
        }

        #endregion


    }
}
