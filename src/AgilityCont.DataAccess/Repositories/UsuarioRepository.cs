using Dapper;
using System;
using System.Data;

namespace AgilityCont.DataAccess
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public UsuarioRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public int AlterarFoto(Usuario entity)
        {
            return _connection.ExecuteScalar<int>(
                "update usuario set foto = @foto where der is null and email = @email",
                new {
                    email = entity.Email,
                    foto = entity.Foto
                }, _transaction);
        }

        public Usuario ObterByEmail(string email)
        {
            return _connection.QueryFirstOrDefault<Usuario>(
                "select * from usuario where der is null and email = @email",
                new { email = email });
        }

        public Usuario ObterById(Guid id)
        {
            return _connection.QueryFirstOrDefault<Usuario>(
                "select * from usuario where der is null and id = @id",
                new { id = id });
        }

        public Usuario Authenticate(string login, string senha)
        {
            var crypt = new EncryptDecrypt();

            return _connection.QueryFirstOrDefault<Usuario>(
                "select * from usuario where der is null and email = @login and senha = @senha",
                new { login = login, senha = crypt.Encrypt(senha) });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
