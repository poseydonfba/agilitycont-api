using System;
using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface ICentroCustoRepository : IDisposable
    {
        IEnumerable<CentroCusto> ObterTodos();
    }
}
