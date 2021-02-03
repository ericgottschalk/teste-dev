using Imposto.Core.Domain.Entites;
using Imposto.Core.Domain.Repositories;
using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Imposto.Core.Data.Xml
{
    public sealed class NotaFiscalRepository : INotaFiscalRepository
    {
        private string XmlPath = ConfigurationManager.AppSettings["NotaFiscalXmlPath"];

        public void Salvar(NotaFiscal notaFiscal)
        {
            var xmlSerializer = new XmlSerializer(typeof(NotaFiscal));

            try
            {
                using (var streamWriter = new StreamWriter(Path.Combine(XmlPath, $"{notaFiscal.NumeroNotaFiscal}.xml")))
                {
                    xmlSerializer.Serialize(streamWriter, notaFiscal);
                }
            }
            catch
            {
                throw new XmlException("Ocorreu um erro ao gerar o xml. Favor verificar o caminho no arquivo de configuração.");
            }
            
        }
    }
}
