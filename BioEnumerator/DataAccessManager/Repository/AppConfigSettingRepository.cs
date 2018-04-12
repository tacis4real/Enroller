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
    internal class AppConfigSettingRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<AppConfigSetting> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public AppConfigSettingRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<AppConfigSetting>(_uoWork);
        }


        #region CRUD


        public Status UpdateAppConfigSettingInfo(AppConfigSetting appConfigSetting)
        {
            try
            {
                var appConfig = _repository.Update(appConfigSetting);
                _uoWork.SaveChanges();
                _status.ReturnedId = appConfig.AppConfigSettingId;
                _status.IsSuccessful = appConfig.AppConfigSettingId > 0;
                return _status;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable update settings ";
                _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
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
                var all = _repository.GetAll();
                return !all.Any() ? new List<AppConfigSetting>() : all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
            
        }

        public AppConfigSetting GetAppConfigSetting(int id)
        {
            try
            {
                var item = _repository.GetById(id);
                return (item == null || item.AppConfigSettingId < 1) ? new AppConfigSetting() : item;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 

        #endregion


        #region Useful Synchronize Function

        //public List<TaxCategory> SyncTaxCategories(RemoteServerAuth authParam, out string msg)
        //{
        //    try
        //    {

        //        var remoteResponse = new RemoteMessanger(RemoteProcessType.GetTaxCategory, authParam, RemoteRequestType.Registration, Method.GET).GetTaxCategory(out msg);
        //        if (remoteResponse == null)
        //        {
        //            return null;
        //        }
        //        if (!remoteResponse.IsSuccessful)
        //        {
        //            msg = "Unable to download Tax Categories from the Remote Server";
        //            return null;
        //        }

        //        if (remoteResponse.TaxCategories.IsNullOrEmpty())
        //        {
        //            msg = "Empty Tax Categories downloaded from the Remote Server";
        //            return null;
        //        }
        //        var myAddList = new List<TaxCategory>();
        //        var myUpdateList = new List<TaxCategory>();

        //        foreach (var item in remoteResponse.TaxCategories)
        //        {
        //            var check = GetRawItem(item.Name.Trim());
        //            if (check != null && check.TaxCategoryId > 0)
        //            {
        //                myUpdateList.Add(new TaxCategory
        //                {
        //                    Name = item.Name,
        //                    Status = item.Status,
        //                    TaxCategoryId = check.TaxCategoryId,
        //                });
        //                continue;
        //            }

        //            myAddList.Add(new TaxCategory
        //            {
        //                Name = item.Name,
        //                Status = item.Status,
        //                TaxCategoryId = item.TaxCategoryId
        //            });

        //        }

        //        var retList = new List<TaxCategory>();
        //        if (myAddList.Any())
        //        {
        //            var processedBusinessTypes = _repository.AddRange(myAddList).ToList();
        //            _uoWork.SaveChanges();
        //            if (!processedBusinessTypes.Any())
        //            {
        //                msg = "Error Occurred! Unable to update local database";
        //                return null;
        //            }
        //            retList.AddRange(processedBusinessTypes);
        //        }

        //        if (myUpdateList.Any())
        //        {
        //            var processedBusinessTypes = _repository.UpdateRange(myUpdateList).ToList();
        //            _uoWork.SaveChanges();
        //            if (!processedBusinessTypes.Any())
        //            {
        //                msg = "Error Occurred! Unable to update local database";
        //                return null;
        //            }
        //            retList.AddRange(processedBusinessTypes);
        //        }

        //        msg = "";
        //        return retList;
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Processing Error Occurred! Please try again later";
        //        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
        //        return null;
        //    }
        //}

        #endregion


    }
}
