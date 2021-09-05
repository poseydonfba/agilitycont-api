using System;
using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface ITipoReceitaRepository : IDisposable
    {
        IEnumerable<TipoReceita> ObterTodos();
    }
}
