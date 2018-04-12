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
    internal class StationInfoService
    {

        private Status _status = new Status()
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly StationInfoRepository _stationInfoRepository;
        public StationInfoService()
        {
            _stationInfoRepository = new StationInfoRepository();
        }


        public List<StationInfo> GetStationInfos()
        {
            try
            {
                return _stationInfoRepository.GetStationInfos() ?? new List<StationInfo>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<StationInfo>();
            }
        }

        public StationInfo GetStationInfo(int stationInfoId)
        {
            try
            {
                return _stationInfoRepository.GetStationInfo(stationInfoId);
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
                return _stationInfoRepository.UpdateStationInfo(corporateInfo, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }

    }
}
