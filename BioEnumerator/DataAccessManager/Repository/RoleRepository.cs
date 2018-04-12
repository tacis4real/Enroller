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
    internal class RoleRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<Role> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public RoleRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<Role>(_uoWork);
        }



        #region CRUD

        

        public List<Role> GetRoles()
        {
            try
            {
                var all = _repository.GetAll().ToList();
                return !all.Any() ? new List<Role>() : all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        public Role GetRole(string roleName)
        {
            try
            {
                var roleLists = _repository.GetAll(x => string.Compare(x.Name, roleName, StringComparison.CurrentCultureIgnoreCase) == 0).ToList();
                if (!roleLists.Any() || roleLists.Count != 1)
                {
                    return new Role();
                }

                return roleLists[0].RoleId < 1 ? new Role() : roleLists[0];
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
