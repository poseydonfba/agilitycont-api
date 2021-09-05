using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AgilityCont.DataAccess
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }

        public TransacaoRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public int Inserir(Transacao entity)
        {
            return _connection.ExecuteScalar<int>(
                "insert into transacao " +
                "(id,tipo_lancamento,tipo_transacao,descricao,data_transacao,valor,desconto,forma_pagamento,idusuario) " +
                "values" +
                "(@Id,@IdTipoLancamento,@IdTipoTransacao,@Descricao,@DataTransacao,@Valor,@Desconto,@IdFormaPagamento,@IdUsuario)",
                entity, _transaction);
        }

        public int Alterar(Transacao entity)
        {
            return _connection.ExecuteScalar<int>(
                "update transacao set " +
                "tipo_lancamento = @IdTipoLancamento, " +
                "tipo_transacao = @IdTipoTransacao, " +
                "descricao = @Descricao, " +
                "data_transacao = @DataTransacao, " +
                "valor = @Valor, " +
                "desconto = @Desconto, " +
                "forma_pagamento = @IdFormaPagamento, " +
                "idusuario = @IdUsuario " +
                "where der is null and id = @Id",
                entity, _transaction);
        }

        public int Excluir(Transacao entity)
        {
            return _connection.ExecuteScalar<int>(
                "update transacao set " +
                "uer = @Uer, " +
                "der = @Der " +
                "where der is null and id = @Id",
                entity, _transaction);
        }

        public Transacao ObterById(Guid id)
        {
            return _connection.QueryFirstOrDefault<Transacao>(
                "select " +
                "id as Id," +
                "tipo_lancamento as IdTipoLancamento," +
                "tipo_transacao as IdTipoTransacao," +
                "descricao as Descricao," +
                "data_transacao as DataTransacao," +
                "valor as Valor," +
                "desconto as Desconto," +
                "forma_pagamento as IdFormaPagamento," +
                "idusuario as IdUsuario " +
                " from transacao where der is null and id = @id",
                new { id = id });
        }

        public IEnumerable<Transacao> ObterPorUsuarioIdByPageIndex(Guid usuarioId, int pageIndex, int pageSize)
        {
            return _connection.Query<Transacao>(@"
                select 
                    t.id as Id,
                    t.tipo_lancamento as IdTipoLancamento,
                    case
                        when t.tipo_lancamento = 1 THEN 'Receita'
                        when t.tipo_lancamento = 2 THEN 'Custo'
                        else 'Despesa'
                    end as DescTipoLancamento,
                    t.tipo_transacao as IdTipoTransacao,
                    case
                        when t.tipo_lancamento = 1 THEN (select a.descricao from tipo_receita a where a.id = t.tipo_transacao)
                        when t.tipo_lancamento = 2 THEN (select a.descricao from centro_custo a where a.id = t.tipo_transacao)
                        else (select a.descricao from tipo_despesa a where a.id = t.tipo_transacao)
                    end as DescTipoTransacao,
                    t.descricao as Descricao,
                    t.data_transacao as DataTransacao,
                    t.valor as Valor,
                    t.desconto as Desconto,
                    t.forma_pagamento as IdFormaPagamento,
                    p.descricao as DescFormaPagamento,
                    t.idusuario as IdUsuario,
                    t.der 
                from transacao t 
                left outer join forma_pagamento p on t.forma_pagamento = p.id
                where t.der is null
                and t.idusuario = @usuarioid
                ", new { usuarioid = usuarioId })
                .OrderByDescending(x => x.DataTransacao)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            //return _connection.Query<Transacao> (@"
            //    select 
            //        t.id as Id,
            //        t.tipo_lancamento as IdTipoLancamento,
            //        case
            //            when t.tipo_lancamento = 1 THEN 'Receita'
            //            when t.tipo_lancamento = 2 THEN 'Custo'
            //            else 'Despesa'
            //        end as DescTipoLancamento,
            //        t.tipo_transacao as IdTipoTransacao,
            //        case
            //            when t.tipo_lancamento = 1 THEN (select a.descricao from tipo_receita a where a.id = t.tipo_transacao)
            //            when t.tipo_lancamento = 2 THEN (select a.descricao from centro_custo a where a.id = t.tipo_transacao)
            //            else (select a.descricao from tipo_despesa a where a.id = t.tipo_transacao)
            //        end as DescTipoTransacao,
            //        t.descricao as Descricao,
            //        t.data_transacao as DataTransacao,
            //        t.valor as Valor,
            //        t.desconto as Desconto,
            //        t.forma_pagamento as IdFormaPagamento,
            //        p.descricao as DescFormaPagamento,
            //        t.idusuario as IdUsuario  
            //    from transacao t 
            //    left outer join forma_pagamento p on t.forma_pagamento = p.id
            //    where t.der is null order by t.data_transacao desc 
            //    Limit @Limit Offset @Offset", 
            //    new { Limit = pageSize, Offset = ((pageIndex - 1) * pageSize) });

            //return _connection.Query<Transacao> (@"
            //    select 
            //    id as Id,
            //    tipo_lancamento as IdTipoLancamento,
            //    tipo_transacao as IdTipoTransacao,
            //    descricao as Descricao,
            //    data_transacao as DataTransacao,
            //    valor as Valor,
            //    desconto as Desconto,
            //    forma_pagamento as IdFormaPagamento,
            //    idusuario as IdUsuario  
            //    from transacao where der is null order by data_transacao desc Limit @Limit Offset @Offset", 
            //    new { Limit = pageSize, Offset = ((pageIndex - 1) * pageSize) });
        }

        public IEnumerable<Transacao> ObterPorUsuarioIdByPageIndex(Guid usuarioId, DateTime dataInicio, DateTime dataFim, int pageIndex, int pageSize)
        {
            return _connection.Query<Transacao>(@"
                select 
                    t.id as Id,
                    t.tipo_lancamento as IdTipoLancamento,
                    case
                        when t.tipo_lancamento = 1 THEN 'Receita'
                        when t.tipo_lancamento = 2 THEN 'Custo'
                        else 'Despesa'
                    end as DescTipoLancamento,
                    t.tipo_transacao as IdTipoTransacao,
                    case
                        when t.tipo_lancamento = 1 THEN (select a.descricao from tipo_receita a where a.id = t.tipo_transacao)
                        when t.tipo_lancamento = 2 THEN (select a.descricao from centro_custo a where a.id = t.tipo_transacao)
                        else (select a.descricao from tipo_despesa a where a.id = t.tipo_transacao)
                    end as DescTipoTransacao,
                    t.descricao as Descricao,
                    t.data_transacao as DataTransacao,
                    t.valor as Valor,
                    t.desconto as Desconto,
                    t.forma_pagamento as IdFormaPagamento,
                    p.descricao as DescFormaPagamento,
                    t.idusuario as IdUsuario,
                    t.der 
                from transacao t 
                left outer join forma_pagamento p on t.forma_pagamento = p.id
                where t.der is null
                and t.idusuario = @usuarioid
                and t.data_transacao >= @dataInicio
                and t.data_transacao <= @dataFim
                ", new { usuarioid = usuarioId, dataInicio = dataInicio, dataFim = dataFim })
                .OrderByDescending(x => x.DataTransacao)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<ExtratoLancamento> ObterExtratoConsolidado(Guid usuarioId, int ano)
        {
            return _connection.Query<ExtratoLancamento>(@"
                select 
	                id as Id,
                    idusuario as IdUsuario,
                    ano as Ano,
                    mes as Mes,sum(receita) as TotalReceita,
                    sum(despesa) as TotalDespesa
                from (
	                select 
		                cast(idusuario as varchar) || extract('year' from t.data_transacao) || extract('month' from t.data_transacao) as id,
		                idusuario, extract('year' from t.data_transacao) as ano, extract('month' from t.data_transacao) as mes,
		                case
		                when t.tipo_lancamento = 1 THEN t.valor
		                else 0
		                end as receita,
		                case
		                when t.tipo_lancamento = 3 THEN t.valor
		                else 0
		                end as despesa
	                from transacao t
	                where t.idusuario = @usuarioid
	                and t.der is null
	                and extract('year' from t.data_transacao) = @ano
                ) as v1
                group by id,idusuario,ano,mes
                order by mes
                ", new { usuarioid = usuarioId, ano = ano });
        }

    }
}
