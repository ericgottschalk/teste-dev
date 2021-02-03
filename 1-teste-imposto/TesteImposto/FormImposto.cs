using Imposto.Core.Domain.Entites;
using Imposto.Core.Domain.FixedValues;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private readonly NotaFiscalApplication _notaFiscalApplication;
        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            BindComboBoxesUf();
            ResizeColumns();

            _notaFiscalApplication = new NotaFiscalApplication();
        }

        private void BindComboBoxesUf()
        {
            comboBoxUfDestino.DataSource = EstadoFixedValue.Todos.Select(t => t.Uf).ToList();
            comboBoxUfOrigem.DataSource = EstadoFixedValue.Todos.Select(t => t.Uf).ToList();
            comboBoxUfDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxUfOrigem.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            var table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(string)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;

            try
            {
                _notaFiscalApplication.GerarNotaFiscal(CriarPedido());
                MessageBox.Show("Operação efetuada com sucesso");
                LimparFormulario();
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro.");
            }         
        }

        private Pedido CriarPedido()
        {
            var pedido = new Pedido
            {
                EstadoOrigem = comboBoxUfOrigem.Text,
                EstadoDestino = comboBoxUfDestino.Text,
                NomeCliente = textBoxNomeCliente.Text
            };

            foreach (DataRow row in ((DataTable)dataGridViewPedidos.DataSource).Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = row["Brinde"] != DBNull.Value ? Convert.ToBoolean(row["Brinde"]) : false,
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = row["Valor"] != DBNull.Value && double.TryParse(row["Valor"].ToString(), out _) ? Convert.ToDouble(row["Valor"]) : 0
                    });
            }

            return pedido;
        }

        public bool Validar()
        {
            if (string.IsNullOrWhiteSpace(textBoxNomeCliente.Text))
            {
                MessageBox.Show("Necessário informar nome do cliente.");
                return false;
            }

            if (((DataTable)dataGridViewPedidos.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("Deve ter pelo menos um item no pedido.");
                return false;
            }

            return true;
        }

        private void LimparFormulario()
        {
            textBoxNomeCliente.Clear();
            comboBoxUfDestino.SelectedIndex = 0;
            comboBoxUfOrigem.SelectedIndex = 0;
            dataGridViewPedidos.DataSource = GetTablePedidos();
        }
    }
}
