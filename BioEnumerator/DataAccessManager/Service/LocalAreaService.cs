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
    internal class LocalAreaService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly LocalAreaRepository _localAreaRepository;
        public LocalAreaService()
        {
            _localAreaRepository = new LocalAreaRepository();
        }


        #region CRUD

        public List<LocalArea> GetLocalAreas()
        {
            try
            {
                var objList = _localAreaRepository.GetLocalAreas();
                if (objList == null) { return new List<LocalArea>(); }
                return objList;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<LocalArea>();
            }
        }

        #endregion
    }
}
