namespace Imposto.Core.Domain.Entites
{
    public class NotaFiscalItem
    {
        public int Id { get; set; }

        public int IdNotaFiscal { get; set; }

        public string Cfop { get; set; }

        public string TipoIcms { get; set; }

        public double BaseIcms { get; set; }

        public double AliquotaIcms { get; set; }

        public double ValorIcms { get; set; }

        public double BaseIpi { get; set; }

        public double AliquotaIpi { get; set; }

        public double ValorIpi { get; set; }

        public string NomeProduto { get; set; }

        public string CodigoProduto { get; set; }

        public double Desconto { get; set; }

        public void CalcularIcms(NotaFiscal notaFiscal, PedidoItem pedidoItem)
        {
            if (pedidoItem.Brinde || notaFiscal.EstadoOrigem == notaFiscal.EstadoDestino)
            {
                TipoIcms = "60";
                AliquotaIcms = 0.18;
            }
            else
            {
                TipoIcms = "10";
                AliquotaIcms = 0.17;
            }

            ValorIcms = BaseIcms * AliquotaIcms;
        }

        public void CalcularIpi(PedidoItem pedidoItem)
        {
            BaseIpi = pedidoItem.ValorItemPedido;
            AliquotaIpi = pedidoItem.Brinde ? 0 : 0.10;
            ValorIpi = BaseIpi * AliquotaIpi;
        }

        public void CalcularDesconto(NotaFiscal notaFiscal)
        {
            if (notaFiscal.EstadoDestino.Regiao == FixedValues.RegiaoEstado.Sudeste)
            {
                Desconto = 0.10;
            }
        }
    }
}
