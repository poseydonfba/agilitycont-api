using AgilityCont.Api.Models;
using AgilityCont.DataAccess;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AgilityCont.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/centrocusto")]
    public class CentroCustoController : ApiController
    {       
        [Route("")]
        [HttpGet]
        public IHttpActionResult ObterTodos()
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var todos = _uow.CentroCustoRepository.ObterTodos();
                    return Ok(todos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
