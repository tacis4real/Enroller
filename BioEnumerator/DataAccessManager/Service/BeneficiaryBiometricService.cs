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
    internal class BeneficiaryBiometricService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly BeneficiaryBiometricRepository _beneficiaryBiometricRepository;

        public BeneficiaryBiometricService()
        {
            _beneficiaryBiometricRepository = new BeneficiaryBiometricRepository();
        }


        public List<BeneficiaryBiometric> GetBeneficiaryBiometrics()
        {
            try
            {
                return _beneficiaryBiometricRepository.GetBeneficiaryBiometrics();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryBiometric>();
            }
        } 


    }
}
