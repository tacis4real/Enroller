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
    internal class StateRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<State> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public StateRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<State>(_uoWork);
        }


        #region CRUD

        public Status AddState(State state)
        {
            try
            {
                var retVal = _repository.Add(state);
                _uoWork.SaveChanges();
                _status.ReturnedId = retVal.StateId;
                _status.IsSuccessful = retVal.StateId > 0;
                return _status;

            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                _status.Message.FriendlyMessage = "Unable to add state";
                _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to add state";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }
        public Status UpdateState(State state)
        {
            try
            {
                var retVal = _repository.Update(state);
                _uoWork.SaveChanges();
                _status.ReturnedId = retVal.StateId;
                _status.IsSuccessful = retVal.StateId > 0;
                return _status;

            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                _status.Message.FriendlyMessage = "Unable to update state";
                _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to update state";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }
        public Status DeleteState(int stateId)
        {
            try
            {
                var state = _repository.Remove(stateId);
                _uoWork.SaveChanges();
                _status.ReturnedId = state.StateId;
                _status.IsSuccessful = state.StateId > 0;
                return _status;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                _status.Message.FriendlyMessage = "Unable to delete state";
                _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to delete state";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }


        public State GetState(int stateId)
        {
            try
            {
                var myItem = _repository.GetById(stateId);
                if (myItem == null || myItem.StateId < 1)
                {
                    return new State();
                }

                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public List<State> GetStates()
        {
            try
            {
                var all = _repository.GetAll().ToList();
                return !all.Any() ? new List<State>() : all.ToList();
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
