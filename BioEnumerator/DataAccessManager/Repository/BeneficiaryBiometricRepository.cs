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
    internal class BeneficiaryBiometricRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };


        private readonly IBioEnumeratorRepository<BeneficiaryBiometric> _repository;
        private readonly BioEnumeratorUoWork _uoWork;

        public BeneficiaryBiometricRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository  = new BioEnumeratorRepository<BeneficiaryBiometric>(_uoWork);
        }



        public BeneficiaryBiometric GetBeneficiaryBiometric(long id)
        {
            try
            {
                var myItem = _repository.GetById(id);
                if (myItem == null || myItem.BeneficiaryBiometricId < 1) return new BeneficiaryBiometric();
                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 

        public List<BeneficiaryBiometric> GetBeneficiaryBiometrics()
        {
            try
            {
                var myItemList = _repository.GetAll();
                if (myItemList == null || !myItemList.Any()) return null;
                return myItemList.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 
    }
}
