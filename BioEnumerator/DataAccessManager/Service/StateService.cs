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
    internal class StateService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly StateRepository _stateRepository;

        public StateService()
        {
            _stateRepository = new StateRepository();
        }


        #region CRUD

        public List<State> GetStates()
        {
            try
            {
                var objList = _stateRepository.GetStates();
                if (objList == null) { return new List<State>(); }
                return objList;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<State>();
            }
        }

        #endregion

    }
}
