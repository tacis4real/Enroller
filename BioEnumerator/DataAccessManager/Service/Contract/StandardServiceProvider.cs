namespace BioEnumerator.DataAccessManager.Service.Contract
{
    internal class StandardServiceProvider : IServiceProvider
    {


        public BeneficiaryService GetBeneficiaryService()
        {
            return new BeneficiaryService();
        }

        public BeneficiaryBiometricService GetBeneficiaryBiometricService()
        {
            return new BeneficiaryBiometricService();
        }

        public UserProfileService GetUserProfileService()
        {
            return new UserProfileService();
        }
        
        public UserService GetUserServices()
        {
            return new UserService();
        }

        public RoleService GetRoleServices()
        {
            return new RoleService();
        }


        public StateService GetStateService()
        {
            return new StateService();
        }

        public LocalAreaService GetLocalAreaService()
        {
            return new LocalAreaService();
        }

        public AppConfigSettingService GetAppConfigSettingService()
        {
            return new AppConfigSettingService();
        }

        public CorporateInfoService GetCorporateInfoService()
        {
            return new CorporateInfoService();
        }

        public StationInfoService GetStationInfoService()
        {
            return new StationInfoService();
        }
        
    }
}
