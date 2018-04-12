namespace BioEnumerator.DataAccessManager.Service.Contract
{
    internal interface IServiceProvider
    {

        BeneficiaryService GetBeneficiaryService();
        BeneficiaryBiometricService GetBeneficiaryBiometricService();
        UserProfileService GetUserProfileService();
        AppConfigSettingService GetAppConfigSettingService();
        UserService GetUserServices();
        RoleService GetRoleServices();
        StateService GetStateService();
        LocalAreaService GetLocalAreaService();
        CorporateInfoService GetCorporateInfoService();
        StationInfoService GetStationInfoService();
        

    }
}
