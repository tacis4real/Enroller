using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataManager;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.Migrations
{
    
    internal partial class Configuration
    {


        private void ConfigHelper()
        {
            SetSqlGenerator("System.Data.SQLite", new xSQLiteMigrationSqlGenerator.SQLiteMigrationSqlGenerator());
        }

        private void ProcessSeed(BioEnumeratorEntities context)
        {
            ProcessRoles(context);
            ProcessLookUpFromFiles(context);
            ProcessStateLookUps(context);
            ProcessAppSettings(context);
        }

        #region Look Ups

        private void ProcessRoles(BioEnumeratorEntities context)
        {
            try
            {
                if (!context.Roles.Any())
                {

                    var role = new Role { Name = "Portal Admin", Status = true };
                    context.Roles.AddOrUpdate(m => m.Name, role);
                    context.SaveChanges();

                    role = new Role { Name = "User Admin", Status = true };
                    context.Roles.AddOrUpdate(m => m.Name, role);
                    context.SaveChanges();

                    role = new Role { Name = "Executive User", Status = true };
                    context.Roles.AddOrUpdate(m => m.Name, role);
                    context.SaveChanges();

                    role = new Role { Name = "Regular User", Status = true };
                    context.Roles.AddOrUpdate(m => m.Name, role);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }
        public void ProcessLookUpFromFiles(BioEnumeratorEntities context)
        {
            try
            {
                var basePath = InternetCon.GetBasePath();
                if (string.IsNullOrEmpty(basePath)) { return; }
                if (!context.LocalAreas.Any())
                {
                    var stateLgas = InternetCon.GetFromResources(basePath + "\\SqlFiles\\lga_lookups.sql");
                    if (!string.IsNullOrEmpty(stateLgas))
                    {
                        context.Database.ExecuteSqlCommand(stateLgas);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }
        private void ProcessStateLookUps(BioEnumeratorEntities context)
        {
            try
            {
                if (!context.States.Any())
                {
                    var state = new State { Name = "Abia" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Adamawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Anambra" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Akwa Ibom" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Bauchi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Bayelsa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Benue" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Borno" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Cross River" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Delta" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ebonyi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Enugu" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Edo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ekiti" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Gombe" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Imo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Jigawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kaduna" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kano" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Katsina" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kebbi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kogi" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Kwara" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Lagos" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Nasarawa" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Niger" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ogun" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Ondo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Osun" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Oyo" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Plateau" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Rivers" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Sokoto" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Taraba" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "Yobe" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();

                    state = new State { Name = "FCT Abuja" };
                    context.States.AddOrUpdate(m => m.Name, state);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void ProcessAppSettings(BioEnumeratorEntities context)
        {
            try
            {
                if (!context.AppConfigSettings.Any())
                {
                    var appSetting = new AppConfigSetting { FirstTimeLaunch = true };
                    context.AppConfigSettings.AddOrUpdate(m => m.AppConfigSettingId, appSetting);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }

        #endregion


    }



}
