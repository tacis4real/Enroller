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
    internal class UserLoginTrailRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<UserLoginTrail> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public UserLoginTrailRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<UserLoginTrail>(_uoWork);
        }


        public int AddUserLoginTrail(UserLoginTrail userLoginTrail)
        {
            try
            {
                var retVal = _repository.Add(userLoginTrail);
                _uoWork.SaveChanges();
                return retVal.UserLoginTrailId;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }




    }
}
