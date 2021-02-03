using Imposto.Core.Domain.Entites;

namespace Imposto.Core.Domain.Services
{
    public sealed class NotaFiscalService
    {
        public NotaFiscal GerarNotaFiscal(Pedido pedido)
        {
            var notaFiscal = new NotaFiscal
            {
                NomeCliente = pedido.NomeCliente,
                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem
            };

            CriarItens(pedido, notaFiscal);

            return notaFiscal;
        }

        private void CriarItens(Pedido pedido, NotaFiscal notaFiscal)
        {
            foreach (var itemPedido in pedido.ItensDoPedido)
            {
                var notaFiscalItem = new NotaFiscalItem
                {
                    NomeProduto = itemPedido.NomeProduto,
                    CodigoProduto = itemPedido.CodigoProduto
                };

                ObterCfop(notaFiscal, notaFiscalItem, itemPedido);

                notaFiscalItem.CalcularIcms(notaFiscal, itemPedido);
                notaFiscalItem.CalcularIpi(itemPedido);
                notaFiscalItem.CalcularDesconto(notaFiscal);

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }

        private void ObterCfop(NotaFiscal notaFiscal, NotaFiscalItem notaFiscalItem, PedidoItem pedidoItem)
        {
            notaFiscalItem.Cfop = notaFiscal.EstadoDestino.Uf switch
            {
                "SP" => notaFiscal.EstadoDestino.Uf switch
                {
                    "RJ" => "6.000",
                    "PE" => "6.001",
                    "MG" => "6.002",
                    "PB" => "6.003",
                    "PR" => "6.004",
                    "PI" => "6.005",
                    "RO" => "6.006",
                    "TO" => "6.008",
                    "SE" => "6.009",
                    "PA" => "6.010",
                    _ => "6.000",
                },
                "MG" => notaFiscal.EstadoDestino.Uf switch
                {
                    "RJ" => "6.000",
                    "PE" => "6.001",
                    "MG" => "6.002",
                    "PB" => "6.003",
                    "PR" => "6.004",
                    "PI" => "6.005",
                    "RO" => "6.006",
                    "TO" => "6.008",
                    "SE" => "6.009",
                    "PA" => "6.010",
                    _ => "6.000",
                },
                _ => "6.000"
            };

            if (notaFiscalItem.Cfop == "6.009")
                notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido * 0.90; //redução de base
            else
                notaFiscalItem.BaseIcms = pedidoItem.ValorItemPedido;
        }
    }
}
