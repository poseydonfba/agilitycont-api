using System;
using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface ITipoDespesaRepository : IDisposable
    {
        IEnumerable<TipoDespesa> ObterTodos();
    }
}
