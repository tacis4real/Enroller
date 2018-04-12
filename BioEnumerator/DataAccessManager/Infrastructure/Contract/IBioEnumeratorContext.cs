using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioEnumerator.DataAccessManager.Infrastructure.Contract
{
    internal interface IBioEnumeratorContext : IDisposable
    {
        DbContext BioEnumeratorDbContext { get; }
    }
}
