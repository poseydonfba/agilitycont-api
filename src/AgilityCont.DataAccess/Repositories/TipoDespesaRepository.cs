using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class TipoDespesaRepository : ITipoDespesaRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public TipoDespesaRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<TipoDespesa> ObterTodos()
        {
            return _connection.Query<TipoDespesa>("select * from tipo_despesa where der is null");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
