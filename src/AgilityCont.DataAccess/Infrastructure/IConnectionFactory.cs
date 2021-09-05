using System;
using System.Data;

namespace AgilityCont.DataAccess
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
    }
}
