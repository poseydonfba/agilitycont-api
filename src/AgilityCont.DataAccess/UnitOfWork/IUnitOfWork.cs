using System;

namespace AgilityCont.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        ICentroCustoRepository CentroCustoRepository { get; }
        IFormaPagamentoRepository FormaPagamentoRepository { get; }
        ITipoDespesaRepository TipoDespesaRepository { get; }
        ITipoReceitaRepository TipoReceitaRepository { get; }
        ITransacaoRepository TransacaoRepository { get; }
        void Commit();
    }
}
