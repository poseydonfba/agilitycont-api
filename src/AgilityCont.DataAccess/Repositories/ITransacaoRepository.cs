using System;
using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface ITransacaoRepository
    {
        int Inserir(Transacao entity);

        int Alterar(Transacao entity);

        int Excluir(Transacao entity);

        Transacao ObterById(Guid id);

        IEnumerable<Transacao> ObterPorUsuarioIdByPageIndex(Guid usuarioId, int pageIndex, int pageSize);

        IEnumerable<Transacao> ObterPorUsuarioIdByPageIndex(Guid usuarioId, DateTime dataInicio, DateTime dataFim, int pageIndex, int pageSize);

        IEnumerable<ExtratoLancamento> ObterExtratoConsolidado(Guid usuarioId, int ano);
    }
}
