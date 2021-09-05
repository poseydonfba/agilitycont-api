using AgilityCont.Api.Models;
using AgilityCont.DataAccess;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace AgilityCont.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/transacao")]
    public class TransacaoController : ApiController
    {
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult ObterPorId(Guid id)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var transacao = _uow.TransacaoRepository.ObterById(id);

                    var model = new TransacaoModel
                    {
                        Id = transacao.Id,
                        IdTipoLancamento = transacao.IdTipoLancamento,
                        IdTipoTransacao = transacao.IdTipoTransacao,
                        Descricao = transacao.Descricao,
                        DataTransacao = transacao.DataTransacao,
                        Valor = transacao.Valor,
                        Desconto = transacao.Desconto,
                        IdFormaPagamento = transacao.IdFormaPagamento,
                        IdUsuario = transacao.IdUsuario
                    };

                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("todos/{pageIndex}/{pageSize}")]
        public IHttpActionResult ObterTodosPorUsuarioId(int pageIndex, int pageSize)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var transacoes = _uow.TransacaoRepository
                        .ObterPorUsuarioIdByPageIndex(Guid.Parse(User.Identity.Name), pageIndex, pageSize)
                        .Select(transacao => new TransacaoModel
                        {
                            Id = transacao.Id,
                            IdTipoLancamento = transacao.IdTipoLancamento,
                            IdTipoTransacao = transacao.IdTipoTransacao,
                            Descricao = transacao.Descricao,
                            DataTransacao = transacao.DataTransacao,
                            Valor = transacao.Valor,
                            Desconto = transacao.Desconto,
                            IdFormaPagamento = transacao.IdFormaPagamento,
                            IdUsuario = transacao.IdUsuario,
                            DescTipoLancamento = transacao.DescTipoLancamento,
                            DescTipoTransacao = transacao.DescTipoTransacao,
                            DescFormaPagamento = transacao.DescFormaPagamento,
                            Uer = transacao.Uer,
                            Der = transacao.Der
                        }).ToList();

                    return Ok(transacoes);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("todos/{dataInicio}/{dataFim}/{pageIndex}/{pageSize}")]
        public IHttpActionResult ObterTodosPorUsuarioId(string dataInicio, string dataFim, int pageIndex, int pageSize)
        {
            try
            {
                var fromDateSearch = DateTime.ParseExact(dataInicio, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var toDateSearch = DateTime.ParseExact(dataFim, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            
                using (var _uow = new UnitOfWork())
                {
                    var transacoes = _uow.TransacaoRepository
                        .ObterPorUsuarioIdByPageIndex(Guid.Parse(User.Identity.Name), fromDateSearch, toDateSearch, pageIndex, pageSize)
                        .Select(transacao => new TransacaoModel
                        {
                            Id = transacao.Id,
                            IdTipoLancamento = transacao.IdTipoLancamento,
                            IdTipoTransacao = transacao.IdTipoTransacao,
                            Descricao = transacao.Descricao,
                            DataTransacao = transacao.DataTransacao,
                            Valor = transacao.Valor,
                            Desconto = transacao.Desconto,
                            IdFormaPagamento = transacao.IdFormaPagamento,
                            IdUsuario = transacao.IdUsuario,
                            DescTipoLancamento = transacao.DescTipoLancamento,
                            DescTipoTransacao = transacao.DescTipoTransacao,
                            DescFormaPagamento = transacao.DescFormaPagamento,
                            Uer = transacao.Uer,
                            Der = transacao.Der
                        }).ToList();

                    return Ok(transacoes);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("extratoconsolidado/{ano}")]
        public IHttpActionResult ObterExtratoConsolidado(int ano)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var transacoes = _uow.TransacaoRepository
                        .ObterExtratoConsolidado(Guid.Parse(User.Identity.Name), ano).ToList();

                    var months = Enumerable.Range(0, 12)
                        .Select(n => new DateTime(ano, 1, 1).AddMonths(n))
                        .Select(d => new { Ano = d.Year, Mes = d.Month, MesDescricao = d.ToString("MMMM") })
                        .OrderBy(o => o.Mes);

                    var joinQuery = (
                                    from m in months
                                    from t in transacoes
                                        .Where(t => m.Ano == t.Ano && m.Mes == t.Mes).DefaultIfEmpty()
                                    select new ExtratoLancamento
                                    {
                                        Id = t == null ? User.Identity.Name + m.Ano.ToString() + m.Mes.ToString() : t.Id,
                                        IdUsuario = t == null ? Guid.Parse(User.Identity.Name) : t.IdUsuario,
                                        Ano = m.Ano,
                                        Mes = m.Mes,
                                        DescMes = m.MesDescricao,
                                        TotalReceita = t == null ? 0 : t.TotalReceita,
                                        TotalDespesa = t == null ? 0 : t.TotalDespesa
                                    }).ToList();

                    return Ok(joinQuery);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Incluir(IncluirTransacaoModel model)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    _uow.TransacaoRepository.Inserir(new Transacao
                    {
                        Id = model.Id,
                        IdTipoLancamento = model.IdTipoLancamento,
                        IdTipoTransacao = model.IdTipoTransacao,
                        Descricao = model.Descricao,
                        DataTransacao = model.DataTransacao,
                        Valor = model.Valor,
                        Desconto = model.Desconto,
                        IdFormaPagamento = model.IdFormaPagamento,
                        IdUsuario = Guid.Parse(User.Identity.Name)
                    });

                    _uow.Commit();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("alterar")]
        public IHttpActionResult Alterar(AlterarTransacaoModel model)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var transacao = _uow.TransacaoRepository.ObterById(model.Id);

                    if (transacao == null)
                        return BadRequest("Transação não existe");

                    if (transacao.IdUsuario != Guid.Parse(User.Identity.Name))
                        return BadRequest("Não foi possível alterar esta transação");

                    transacao.IdTipoLancamento = model.IdTipoLancamento;
                    transacao.IdTipoTransacao = model.IdTipoTransacao;
                    transacao.Descricao = model.Descricao;
                    transacao.DataTransacao = model.DataTransacao;
                    transacao.Valor = model.Valor;
                    transacao.Desconto = model.Desconto;
                    transacao.IdFormaPagamento = model.IdFormaPagamento;

                    _uow.TransacaoRepository.Alterar(transacao);

                    _uow.Commit();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("excluir")]
        public IHttpActionResult Excluir(ExcluirTransacaoModel model)
        {
            try
            {
                using (var _uow = new UnitOfWork())
                {
                    var transacao = _uow.TransacaoRepository.ObterById(model.Id);

                    if (transacao == null)
                        return BadRequest("Transação não existe");

                    if (transacao.IdUsuario != Guid.Parse(User.Identity.Name))
                        return BadRequest("Não foi possível excluir esta transação");

                    transacao.Uer = Guid.Parse(User.Identity.Name);
                    transacao.Der = DateTime.Now;

                    _uow.TransacaoRepository.Excluir(transacao);

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
