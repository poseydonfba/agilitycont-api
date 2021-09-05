//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace AgilityCont.DataAccess
//{
//    public class UsuarioService : IUsuarioService
//    {
//        IUnitOfWork _unitOfWork;

//        public UsuarioService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task AlterarFoto(string email, string base64)
//        {
//            await _unitOfWork.UsuarioRepository.AlterarFoto(email, base64);
//        }

//        public async Task<Usuario> ObterByEmail(string email)
//        {
//            return await _unitOfWork.UsuarioRepository.ObterByEmail(email);
//        }

//        public async Task<IEnumerable<Usuario>> ObterByPageIndex(int pageIndex, int pageSize)
//        {
//            return await _unitOfWork.UsuarioRepository.ObterByPageIndex(pageIndex, pageSize);
//        }

//        public void Dispose()
//        {
//            _unitOfWork.Dispose();
//        }
//    }
//}
