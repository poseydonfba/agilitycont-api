using AgilityCont.Api.Models;
using AgilityCont.DataAccess;
using System;
using System.Web.Http;

namespace AgilityCont.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/usuario")]
    public class UsuarioController : ApiController
    {

        [Route("UserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var user = _uow.UsuarioRepository.ObterById(Guid.Parse(User.Identity.Name));

                    var usuario = new UsuarioModel
                    {
                        Id = user.Id,
                        Nome = user.Nome,
                        Email = user.Email,
                        Chave = user.Chave,
                        Ativo = user.Ativo,
                        Tipo = user.Tipo,
                        Foto = user.Foto
                    };

                    return Ok(usuario);
                }                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AlterarFoto")]
        public IHttpActionResult AlterarFoto(AlterarFotoModel model)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var user = _uow.UsuarioRepository.ObterById(Guid.Parse(User.Identity.Name));
                    user.Foto = model.Foto;

                    _uow.UsuarioRepository.AlterarFoto(user);

                    _uow.Commit();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
