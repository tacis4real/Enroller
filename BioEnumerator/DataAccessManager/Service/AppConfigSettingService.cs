using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Repository;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Service
{
    internal class AppConfigSettingService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly AppConfigSettingRepository _appConfigSettingRepository;
        public AppConfigSettingService()
        {
            _appConfigSettingRepository = new AppConfigSettingRepository();
        }


        #region CRUD


        public Status UpdateAppConfigSettingInfo(AppConfigSetting appConfigSetting)
        {
            try
            {
                return _appConfigSettingRepository.UpdateAppConfigSettingInfo(appConfigSetting);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable update settings ";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }


        public List<AppConfigSetting> GetAppConfigSettings()
        {
            try
            {
                return _appConfigSettingRepository.GetAppConfigSettings() ?? new List<AppConfigSetting>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<AppConfigSetting>();
            }
        }

        public AppConfigSetting GetAppConfigSetting(int id)
        {
            try
            {
                return _appConfigSettingRepository.GetAppConfigSetting(id) ?? new AppConfigSetting();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 


        #endregion
    }
}
