using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Extensions;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal class StationInfoRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<StationInfo> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public StationInfoRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<StationInfo>(_uoWork);
        }


        #region CRUD


        public List<StationInfo> GetStationInfos()
        {
            try
            {
                var all = _repository.GetAll();
                if (all == null || !all.Any())
                    return  new List<StationInfo>();
                return all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        public StationInfo GetStationInfo(int stationInfoId)
        {
            try
            {
                var myItem = _repository.GetById(stationInfoId);
                if (myItem == null || myItem.StationInfoId < 1)
                    return new StationInfo();
                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new StationInfo();
            }
        }

        public bool UpdateStationInfo(StationInfo corporateInfo, out string msg)
        {
            try
            {
                #region Model Validation

                // Validation

                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(corporateInfo, out valResults))
                {
                    var errorDetail = new StringBuilder();
                    if (!valResults.IsNullOrEmpty())
                    {
                        errorDetail.AppendLine("Following error occurred:");
                        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                    }
                    else
                    {
                        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                        msg = errorDetail.ToString();
                        return false;
                    }
                }

                #endregion

                var stationInfo = _repository.Update(corporateInfo);
                _uoWork.SaveChanges();
                msg = "";
                return stationInfo.StationInfoId > 0;
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }

        #endregion

    }
}
