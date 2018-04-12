using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal class LocalAreaRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<LocalArea> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public LocalAreaRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<LocalArea>(_uoWork);
        }


        #region CRUD

        

        public List<LocalArea> GetLocalAreas()
        {
            try
            {
                var all = _repository.GetAll().ToList();
                return !all.Any() ? new List<LocalArea>() : all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public LocalArea GetLocalArea(int localAreaId)
        {
            try
            {
                var myItem = _repository.GetById(localAreaId);
                if (myItem == null || myItem.LocalAreaId < 1)
                {
                    return new LocalArea();
                }

                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public LocalArea GetLocalAreaByStateId(int stateId)
        {
            try
            {
                var myItem = _repository.GetAll(x => x.StateId == stateId).ToList();
                if (!myItem.Any())
                {
                    return new LocalArea();
                }

                return myItem[0];
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
