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
    internal class RoleService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly RoleRepository _roleRepository;
        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }

        public Role GetRole(string roleName)
        {
            try
            {
                var myItem = _roleRepository.GetRole(roleName);
                if (myItem == null || myItem.RoleId < 1) { return new Role(); }
                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new Role();
            }
        }
    }
}
