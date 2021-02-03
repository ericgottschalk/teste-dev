using System.Collections.Generic;
using Imposto.Core.Domain.FixedValues;

namespace Imposto.Core.Domain.Entites
{
    public class Pedido
    {
        public Pedido()
        {
            ItensDoPedido = new List<PedidoItem>();
        }

        public Estado EstadoDestino { get; set; }

        public Estado EstadoOrigem { get; set; }

        public string NomeCliente { get; set; }

        public List<PedidoItem> ItensDoPedido { get; set; }
    }
}
