using System;
using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface IFormaPagamentoRepository : IDisposable
    {
        IEnumerable<FormaPagamento> ObterTodos();
    }
}
