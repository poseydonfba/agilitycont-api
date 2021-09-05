using Npgsql;
using System.Configuration;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUsuarioRepository _usuarioRepository;
        private ICentroCustoRepository _centroCustoRepository;
        private IFormaPagamentoRepository _formaPagamentoRepository;
        private ITipoDespesaRepository _tipoDespesaRepository;
        private ITipoReceitaRepository _tipoReceitaRepository;
        private ITransacaoRepository _transacaoRepository;

        private readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public UnitOfWork()
        {
            _connection = new NpgsqlConnection(connectionString); 
            _connection.Open(); 
            _transaction = _connection.BeginTransaction(); 
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                return _usuarioRepository ?? (_usuarioRepository = new UsuarioRepository(_transaction));
            }
        }

        public ICentroCustoRepository CentroCustoRepository
        {
            get
            {
                return _centroCustoRepository ?? (_centroCustoRepository = new CentroCustoRepository(_transaction));
            }
        }

        public IFormaPagamentoRepository FormaPagamentoRepository
        {
            get
            {
                return _formaPagamentoRepository ?? (_formaPagamentoRepository = new FormaPagamentoRepository(_transaction));
            }
        }

        public ITipoDespesaRepository TipoDespesaRepository
        {
            get
            {
                return _tipoDespesaRepository ?? (_tipoDespesaRepository = new TipoDespesaRepository(_transaction));
            }
        }

        public ITipoReceitaRepository TipoReceitaRepository
        {
            get
            {
                return _tipoReceitaRepository ?? (_tipoReceitaRepository = new TipoReceitaRepository(_transaction));
            }
        }

        public ITransacaoRepository TransacaoRepository
        {
            get
            {
                return _transacaoRepository ?? (_transacaoRepository = new TransacaoRepository(_transaction));
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        private void ResetRepositories()
        {
            _usuarioRepository = null;
        }
    }
}
