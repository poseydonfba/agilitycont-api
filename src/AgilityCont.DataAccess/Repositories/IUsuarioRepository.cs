using System;

namespace AgilityCont.DataAccess
{
    public interface IUsuarioRepository : IDisposable
    {
        int AlterarFoto(Usuario entity);

        Usuario ObterByEmail(string email);

        Usuario ObterById(Guid id);

        Usuario Authenticate(string login, string senha);
    }
}
