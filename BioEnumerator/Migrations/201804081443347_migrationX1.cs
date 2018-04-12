namespace BioEnumerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationX1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppConfigSetting",
                c => new
                    {
                        AppConfigSettingId = c.Int(nullable: false, identity: true),
                        FirstTimeLaunch = c.Boolean(nullable: false),
                        TimeStampConfigured = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppConfigSettingId);
            
            CreateTable(
                "dbo.CompanyInfo",
                c => new
                    {
                        CompanyInfoId = c.Int(nullable: false, identity: true),
                        StationName = c.String(nullable: false, maxLength: 100),
                        StationKey = c.String(nullable: false, maxLength: 100),
                        HostServerAddress = c.String(nullable: false, maxLength: 500),
                        Address = c.String(maxLength: 2147483647),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyInfoId);
            
            CreateTable(
                "dbo.LocalArea",
                c => new
                    {
                        LocalAreaId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Name = c.String(maxLength: 2147483647),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocalAreaId)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.Beneficiary",
                c => new
                    {
                        BeneficiaryId = c.Long(nullable: false, identity: true),
                        BeneficiaryRemoteId = c.Long(nullable: false),
                        RecordId = c.Int(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        Othernames = c.String(maxLength: 2147483647),
                        DateOfBirth = c.String(nullable: false, maxLength: 10),
                        MobileNumber = c.String(maxLength: 2147483647),
                        ResidentialAddress = c.String(nullable: false, maxLength: 200),
                        OfficeAddress = c.String(maxLength: 200),
                        StateId = c.Int(nullable: false),
                        LocalAreaId = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        OccupationId = c.Int(nullable: false),
                        TimeStampRegistered = c.String(nullable: false, maxLength: 35),
                        Status = c.Int(nullable: false),
                        TimeStampUploaded = c.String(maxLength: 2147483647),
                        UploadStatus = c.Int(nullable: false),
                        UploadMessage = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.BeneficiaryId)
                .ForeignKey("dbo.LocalArea", t => t.LocalAreaId, cascadeDelete: true)
                .Index(t => t.LocalAreaId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserProfileId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 50),
                        IsFirstTimeLogin = c.Boolean(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        LastLockedOutTimeStamp = c.String(maxLength: 35),
                        LastLoginTimeStamp = c.String(maxLength: 35),
                        LastPasswordChangedTimeStamp = c.String(maxLength: 35),
                        Password = c.String(nullable: false, maxLength: 50),
                        RegisteredDateTimeStamp = c.String(nullable: false, maxLength: 35),
                        RoleId = c.Int(nullable: false),
                        Salt = c.String(maxLength: 2147483647),
                        UserCode = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserProfileId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserLoginTrail",
                c => new
                    {
                        UserLoginTrailId = c.Int(nullable: false, identity: true),
                        LoginSource = c.String(nullable: false, maxLength: 20),
                        IsSuccessful = c.Boolean(nullable: false),
                        LoginTimeStamp = c.String(maxLength: 25),
                        UserProfileId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserLoginTrailId)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        StationInfoId = c.Int(nullable: false),
                        ProfileNumber = c.String(nullable: false, maxLength: 2147483647),
                        UserProfileRemoteId = c.Long(nullable: false),
                        Surname = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        OtherNames = c.String(maxLength: 2147483647),
                        Sex = c.Int(nullable: false),
                        ResidentialAddress = c.String(maxLength: 100),
                        Email = c.String(maxLength: 50),
                        MobileNumber = c.String(nullable: false, maxLength: 15),
                        DateLastModified = c.String(nullable: false, maxLength: 10),
                        TimeLastModified = c.String(nullable: false, maxLength: 15),
                        ModifiedBy = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.StationInfo",
                c => new
                    {
                        StationInfoId = c.Int(nullable: false, identity: true),
                        RemoteStationId = c.Long(nullable: false),
                        StationName = c.String(nullable: false, maxLength: 100),
                        StationKey = c.String(nullable: false, maxLength: 2147483647),
                        HostServerAddress = c.String(nullable: false, maxLength: 500),
                        APIAccessKey = c.String(maxLength: 10),
                        TimeStampRegistered = c.String(nullable: false, maxLength: 35),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StationInfoId);
            
            CreateTable(
                "dbo.BeneficiaryBiometric",
                c => new
                    {
                        BeneficiaryBiometricId = c.Long(nullable: false, identity: true),
                        BeneficiaryId = c.Long(nullable: false),
                        FingerTemplate = c.String(maxLength: 2147483647),
                        ImageFileName = c.String(maxLength: 2147483647),
                        ImagePath = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.BeneficiaryBiometricId)
                .ForeignKey("dbo.Beneficiary", t => t.BeneficiaryId, cascadeDelete: true)
                .Index(t => t.BeneficiaryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BeneficiaryBiometric", "BeneficiaryId", "dbo.Beneficiary");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.User", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.UserLoginTrail", "User_UserId", "dbo.User");
            DropForeignKey("dbo.LocalArea", "StateId", "dbo.State");
            DropForeignKey("dbo.Beneficiary", "LocalAreaId", "dbo.LocalArea");
            DropIndex("dbo.BeneficiaryBiometric", new[] { "BeneficiaryId" });
            DropIndex("dbo.UserLoginTrail", new[] { "User_UserId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "UserProfileId" });
            DropIndex("dbo.Beneficiary", new[] { "LocalAreaId" });
            DropIndex("dbo.LocalArea", new[] { "StateId" });
            DropTable("dbo.BeneficiaryBiometric");
            DropTable("dbo.StationInfo");
            DropTable("dbo.UserProfile");
            DropTable("dbo.UserLoginTrail");
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.State");
            DropTable("dbo.Beneficiary");
            DropTable("dbo.LocalArea");
            DropTable("dbo.CompanyInfo");
            DropTable("dbo.AppConfigSetting");
        }
    }
}
