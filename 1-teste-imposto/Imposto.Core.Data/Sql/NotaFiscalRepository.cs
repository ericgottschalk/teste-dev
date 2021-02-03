using Imposto.Core.Domain.Entites;
using Imposto.Core.Domain.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Imposto.Core.Data.Sql
{
    public sealed class NotaFiscalRepository : INotaFiscalRepository
    {
        private const string ProcNotaFiscal = "dbo.P_NOTA_FISCAL";
        private const string ProcNotaFiscalItem = "dbo.P_NOTA_FISCAL_ITEM";

        private string ConnectionString => ConfigurationManager.ConnectionStrings["NotaFiscalConnection"].ConnectionString;
        
        public void Salvar(NotaFiscal notaFiscal)
        {
            SalvarNotaFiscal(notaFiscal);

            foreach (var item in notaFiscal.ItensDaNotaFiscal)
            {
                SalvarNotaFiscalItem(notaFiscal.Id, item);
            }
        }

        private void SalvarNotaFiscal(NotaFiscal notaFiscal)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(ProcNotaFiscal, connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@pId", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@pNumeroNotaFiscal", SqlDbType.Int).Value = notaFiscal.NumeroNotaFiscal;
                command.Parameters.Add("@pSerie", SqlDbType.Int).Value = notaFiscal.Serie;
                command.Parameters.Add("@pNomeCliente", SqlDbType.VarChar).Value = notaFiscal.NomeCliente;
                command.Parameters.Add("@pEstadoDestino", SqlDbType.VarChar).Value = notaFiscal.EstadoDestino.ToString();
                command.Parameters.Add("@pEstadoOrigem", SqlDbType.VarChar).Value = notaFiscal.EstadoOrigem.ToString();

                command.ExecuteNonQuery();
                notaFiscal.Id = Convert.ToInt32(command.Parameters["@pId"].Value);
            }
        }

        private void SalvarNotaFiscalItem(int notaFiscalId, NotaFiscalItem item)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(ProcNotaFiscalItem, connection))
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@pIdNotaFiscal", SqlDbType.Int).Value = notaFiscalId;
                command.Parameters.Add("@pCfop", SqlDbType.VarChar).Value = item.Cfop;
                command.Parameters.Add("@pTipoIcms", SqlDbType.VarChar).Value = item.TipoIcms;
                command.Parameters.Add("@pBaseIcms", SqlDbType.Decimal).Value = item.BaseIcms;
                command.Parameters.Add("@pAliquotaIcms", SqlDbType.Decimal).Value = item.AliquotaIcms;
                command.Parameters.Add("@pValorIcms", SqlDbType.Decimal).Value = item.ValorIcms;
                command.Parameters.Add("@pBaseIpi", SqlDbType.Decimal).Value = item.BaseIpi;
                command.Parameters.Add("@pAliquotaIpi", SqlDbType.Decimal).Value = item.AliquotaIpi;
                command.Parameters.Add("@pValorIpi", SqlDbType.Decimal).Value = item.ValorIpi;
                command.Parameters.Add("@pNomeProduto", SqlDbType.VarChar).Value = item.NomeProduto;
                command.Parameters.Add("@pCodigoProduto", SqlDbType.VarChar).Value = item.CodigoProduto;
                command.Parameters.Add("@pDesconto", SqlDbType.Decimal).Value = item.Desconto;

                command.ExecuteNonQuery();
            }
        }
    }
}
