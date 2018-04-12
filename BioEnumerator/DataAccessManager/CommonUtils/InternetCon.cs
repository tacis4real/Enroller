using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataManager;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.CommonUtils
{
    public class InternetCon
    {
        public static SQLiteConnection GetConnString(string databaseFilePath)
        {
            return new SQLiteConnection
            {
                ConnectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = databaseFilePath,
                    DefaultTimeout = 5000,
                    SyncMode = SynchronizationModes.Off,
                    JournalMode = SQLiteJournalModeEnum.Memory,
                    PageSize = 65536,
                    CacheSize = 16777216,
                    FailIfMissing = false,
                    ReadOnly = false,
                    Password = "Pa33w0rd123"
                }.ConnectionString
            };
        }
        public static string GetBasePath()
        {
            var currentDomain = AppDomain.CurrentDomain;

            string dirPath;
            if (currentDomain.BaseDirectory.Contains("\\bin\\Debug"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "");
            }
            else if (currentDomain.BaseDirectory.Contains("\\bin\\Release"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Release\\", "");
            }
            else
            {
                dirPath = currentDomain.BaseDirectory;
            }
            return dirPath;
        }
        public static string GetFromResources(string resourceName)
        {
            try
            {
                using (var reader = new StreamReader(resourceName))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ProcessLookUpFromFiles(BioEnumeratorEntities context)
        {
            try
            {
                var basePath = GetBasePath();
                if (string.IsNullOrEmpty(basePath)) { return ""; }
                if (!context.LocalAreas.Any())
                {
                    var stateLgas = GetFromResources(basePath + "\\SqlFiles\\lga_lookup.sql");
                    if (!string.IsNullOrEmpty(stateLgas))
                    {
                        context.Database.ExecuteSqlCommand(stateLgas);
                    }
                }

                return basePath;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

    }
}
