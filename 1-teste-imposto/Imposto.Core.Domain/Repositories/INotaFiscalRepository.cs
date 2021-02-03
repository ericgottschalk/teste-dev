using Imposto.Core.Domain.Entites;

namespace Imposto.Core.Domain.Repositories
{
    public interface INotaFiscalRepository
    {
        void Salvar(NotaFiscal notaFiscal);
    }
}
