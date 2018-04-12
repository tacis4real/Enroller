using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal  class CorporateInfoRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<CompanyInfo> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public CorporateInfoRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<CompanyInfo>(_uoWork);
        }


        #region CRUD

        public Status AddCorporateInfo(CompanyInfo corporateInfo)
        {
            try
            {
                var companyInfo = _repository.Add(corporateInfo);
                _uoWork.SaveChanges();
                _status.ReturnedId = companyInfo.CompanyInfoId;
                _status.IsSuccessful = companyInfo.CompanyInfoId > 0;
                return _status;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to add company information";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to add company information";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }

        public CompanyInfo GetCorporateInfo(int corporateInfoId)
        {
            try
            {
                var byId = _repository.GetById(corporateInfoId);
                if (byId == null || byId.CompanyInfoId < 1)
                    return new CompanyInfo();
                return byId;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        public List<CompanyInfo> GetCorporateInfoes()
        {
            try
            {
                var all = _repository.GetAll();
                if (all == null || !all.Any())
                    return new List<CompanyInfo>();
                return all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<CompanyInfo>();
            }
        }

        #endregion

    }
}
