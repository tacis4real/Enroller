namespace BioEnumerator.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed partial class Configuration : DbMigrationsConfiguration<DataAccessManager.DataManager.BioEnumeratorEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ConfigHelper();
        }

        protected override void Seed(DataAccessManager.DataManager.BioEnumeratorEntities context)
        {
            ProcessSeed(context);
        }
    }
}
