using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class CentroCustoRepository : ICentroCustoRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public CentroCustoRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<CentroCusto> ObterTodos()
        {
            return _connection.Query<CentroCusto>("select * from centro_custo where der is null");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
