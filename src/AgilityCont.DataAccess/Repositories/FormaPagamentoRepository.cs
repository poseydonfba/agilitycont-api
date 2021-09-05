using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public FormaPagamentoRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<FormaPagamento> ObterTodos()
        {
            return _connection.Query<FormaPagamento>("select * from forma_pagamento where der is null");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
