using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class TipoReceitaRepository : ITipoReceitaRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public TipoReceitaRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<TipoReceita> ObterTodos()
        {
            return _connection.Query<TipoReceita>("select * from tipo_receita where der is null");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
