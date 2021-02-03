using Imposto.Core.Domain.Entites;
using Imposto.Core.Domain.Repositories;
using Imposto.Core.Domain.Services;

namespace TesteImposto
{
    public sealed class NotaFiscalApplication
    {
        private readonly NotaFiscalService _notaFiscalService;
        private readonly INotaFiscalRepository _notaFiscalSqlRepository;
        private readonly INotaFiscalRepository _notaFiscalXmlRepository;

        public NotaFiscalApplication()
        {
            _notaFiscalService = new NotaFiscalService();
            _notaFiscalSqlRepository = new Imposto.Core.Data.Sql.NotaFiscalRepository();
            _notaFiscalXmlRepository = new Imposto.Core.Data.Xml.NotaFiscalRepository();
        }

        public void GerarNotaFiscal(Pedido pedido)
        {
            var notaFiscal = _notaFiscalService.GerarNotaFiscal(pedido);

            _notaFiscalXmlRepository.Salvar(notaFiscal);
            _notaFiscalSqlRepository.Salvar(notaFiscal);
        }
    }
}
