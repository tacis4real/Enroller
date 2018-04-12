using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Repository;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Service
{
    internal class CorporateInfoService
    {

        private Status _status = new Status()
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly CorporateInfoRepository _corporateInfoRepository;
        public CorporateInfoService()
        {
            _corporateInfoRepository = new CorporateInfoRepository();
        }



        public Status AddCompanyInfo(CompanyInfo companyInfo)
        {
            try
            {
                return _corporateInfoRepository.AddCorporateInfo(companyInfo);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable to add new user record";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }

        public CompanyInfo GetCompanyInfo(int companyInfoId)
        {
            try
            {
                return _corporateInfoRepository.GetCorporateInfo(companyInfoId);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new CompanyInfo();
            }
        }

        public List<CompanyInfo> GetCompanyInfos()
        {
            try
            {
                return _corporateInfoRepository.GetCorporateInfoes()
                    ?? new List<CompanyInfo>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<CompanyInfo>();
            }
        }

    }
}
