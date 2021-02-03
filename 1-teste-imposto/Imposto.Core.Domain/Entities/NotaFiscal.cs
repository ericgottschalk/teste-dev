using Imposto.Core.Domain.FixedValues;
using System;
using System.Collections.Generic;

namespace Imposto.Core.Domain.Entites
{
    public class NotaFiscal
    {
        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
            NumeroNotaFiscal = new Random().Next(int.MaxValue);
            Serie = new Random().Next(int.MaxValue);
        }

        public int Id { get; set; }

        public int NumeroNotaFiscal { get; set; }

        public int Serie { get; set; }

        public string NomeCliente { get; set; }

        public Estado EstadoDestino { get; set; }

        public Estado EstadoOrigem { get; set; }

        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }
    }
}
