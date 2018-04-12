using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;

namespace BioEnumerator.DataAccessManager.DataManager
{
    public partial class BioEnumeratorEntities : DbContext
    {

        private static string _path = InternetCon.GetBasePath() + "\\AppFiles\\" + "BioEnumeratorDB.db";

        public BioEnumeratorEntities()
            : base(InternetCon.GetConnString(_path), true)
        {
            Configuration.ProxyCreationEnabled = false;
        }


        #region Models

        #region User Manager
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLoginTrail> UserLoginTrails { get; set; }
        public DbSet<StationInfo> StationInfos { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        #endregion

        #region Enumerator

        public DbSet<AppConfigSetting> AppConfigSettings { get; set; } 
        public DbSet<State> States { get; set; }
        public DbSet<LocalArea> LocalAreas { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BioEnumeratorEntities>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            
            #region Relationship - Fluent API

            modelBuilder.Entity<Role>()
                .HasMany(x => x.Users)
                .WithRequired(x => x.Role)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserLoginTrails)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>().HasRequired(x => x.UserProfile);

            modelBuilder.Entity<BeneficiaryBiometric>().HasRequired(x => x.Beneficiary);

            //modelBuilder.Entity<Beneficiary>()
            //    .HasRequired(x => x.BeneficiaryBiometric).WithRequiredDependent()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Beneficiary>()
            //    .HasRequired(x => x.BeneficiaryBiometric)
            //    .WithRequiredDependent()
            //    //.WithRequired(x => x.User)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Beneficiary>().HasRequired(x => x.BeneficiaryBiometric);
            
            //modelBuilder.Entity<Beneficiary>()
            //    .HasRequired(x => x.BeneficiaryBiometric)
            //    .WithRequiredPrincipal()
            //    .WillCascadeOnDelete(false);

            //ntext
            modelBuilder.Entity<BeneficiaryBiometric>()
               .Property(x => x._Template)
               .HasColumnName("FingerTemplate")
               .IsMaxLength()
               //.HasColumnName("FingerTemplate").HasColumnType("ntext")
               .IsOptional();

            #endregion


        }
    }
}
