using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataManager;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using BioEnumerator.Migrations;

namespace BioEnumerator.DataAccessManager.Infrastructure
{
    public class BioEnumeratorContext : IBioEnumeratorContext
    {

        public BioEnumeratorContext()
        {
            BioEnumeratorDbContext = new BioEnumeratorEntities();
            BioEnumeratorDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public BioEnumeratorContext(string conString)
        {
            BioEnumeratorDbContext = new DbContext(ConfigurationManager.ConnectionStrings[conString].ConnectionString);
            BioEnumeratorDbContext.Configuration.LazyLoadingEnabled = false;
        }

        
        public void Dispose()
        {
            BioEnumeratorDbContext.Dispose();
        }

        public DbContext BioEnumeratorDbContext { get; private set; }
    }
}
