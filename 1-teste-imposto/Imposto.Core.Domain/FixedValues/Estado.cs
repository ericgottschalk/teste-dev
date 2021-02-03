using System;
using System.Linq;

namespace Imposto.Core.Domain.FixedValues
{
    public sealed class Estado
    {
        private Estado()
        {
        }

        public Estado(int id, string uf, RegiaoEstado regiao)
        {
            Id = id;
            Uf = uf;
            Regiao = regiao;
        }

        public int Id { get; set; }

        public string Uf { get; set; }

        public RegiaoEstado Regiao { get; set; }


        public static implicit operator Estado(string uf)
        {
            try
            {
                return EstadoFixedValue.Todos.Single(x => x.Uf == uf.ToUpper());
            }
            catch (Exception)
            {
                throw new InvalidCastException($"UF invalida: {uf}.");
            }
        }

        public static bool operator ==(Estado left, Estado right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Estado left, Estado right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => Uf;

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Uf == (obj as Estado).Uf;
        }
    }

    public enum RegiaoEstado
    {
        CentroOeste,
        Norte,
        Nordeste,
        Sul,
        Sudeste
    }

    public sealed class EstadoFixedValue
    {
        public static readonly Estado AC = new Estado(1, "AC", RegiaoEstado.Norte);
        public static readonly Estado AL = new Estado(2, "AL", RegiaoEstado.Nordeste);
        public static readonly Estado AP = new Estado(3, "AP", RegiaoEstado.Norte);
        public static readonly Estado AM = new Estado(4, "AM", RegiaoEstado.Norte);
        public static readonly Estado BA = new Estado(5, "BA", RegiaoEstado.Nordeste);
        public static readonly Estado CE = new Estado(6, "CE", RegiaoEstado.Nordeste);
        public static readonly Estado DF = new Estado(7, "DF", RegiaoEstado.CentroOeste);
        public static readonly Estado ES = new Estado(8, "ES", RegiaoEstado.Sudeste);
        public static readonly Estado GO = new Estado(9, "GO", RegiaoEstado.CentroOeste);
        public static readonly Estado MA = new Estado(10, "MA", RegiaoEstado.Nordeste);
        public static readonly Estado MT = new Estado(11, "MT", RegiaoEstado.CentroOeste);
        public static readonly Estado MS = new Estado(12, "MS", RegiaoEstado.CentroOeste);
        public static readonly Estado MG = new Estado(13, "MG", RegiaoEstado.Sudeste);
        public static readonly Estado PA = new Estado(14, "PA", RegiaoEstado.Norte);
        public static readonly Estado PB = new Estado(15, "PB", RegiaoEstado.Nordeste);
        public static readonly Estado PR = new Estado(16, "PR", RegiaoEstado.Sul);
        public static readonly Estado PE = new Estado(17, "PE", RegiaoEstado.Nordeste);
        public static readonly Estado PI = new Estado(18, "PI", RegiaoEstado.Nordeste);
        public static readonly Estado RJ = new Estado(19, "RJ", RegiaoEstado.Sudeste);
        public static readonly Estado RN = new Estado(20, "RN", RegiaoEstado.Nordeste);
        public static readonly Estado RS = new Estado(21, "RS", RegiaoEstado.Sul);
        public static readonly Estado RO = new Estado(22, "RO", RegiaoEstado.Norte);
        public static readonly Estado RR = new Estado(23, "RR", RegiaoEstado.Norte);
        public static readonly Estado SC = new Estado(24, "SC", RegiaoEstado.Sul);
        public static readonly Estado SP = new Estado(25, "SP", RegiaoEstado.Sudeste);
        public static readonly Estado SE = new Estado(26, "SE", RegiaoEstado.Nordeste);
        public static readonly Estado TO = new Estado(27, "TO", RegiaoEstado.Norte);

        public static readonly Estado[] Todos = new Estado[]
        {
            AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA, PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO
        };
    }
}
