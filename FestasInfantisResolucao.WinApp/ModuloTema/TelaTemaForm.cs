using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.Dominio.ModuloTema;
using FestasInfantisResolucao.WinApp.Compartilhado;

namespace FestasInfantisResolucao.WinApp.ModuloTema
{
    public partial class TelaTemaForm : Form
    {
        private List<Item> itensCadastrados;

        public TelaTemaForm(List<Item> itensCadastrados)
        {
            InitializeComponent();

            ConfigurarColunas();

            this.itensCadastrados = itensCadastrados;
        }

        public Tema ObterTema()
        {
            int id = Convert.ToInt32(txtId.Text);

            string nome = txtNome.Text;

            List<Item> itens = new List<Item>();

            foreach (DataGridViewRow linha in gridItens.Rows)
            {
                int idItem = Convert.ToInt32(linha.Cells[0].Value);
                string descricao = Convert.ToString(linha.Cells[1].Value)!;
                decimal valor = Convert.ToDecimal(linha.Cells[2].Value);

                Item itemSelecionado = new Item(idItem, descricao, valor);

                itens.Add(itemSelecionado);
            }

            Tema tema = new Tema(nome, itens);

            if (id > 0)
                tema.id = id;

            return tema;
        }

        private void ConfigurarColunas()
        {
            DataGridViewColumn[] colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn()
                {
                    Name = "id",
                    HeaderText = "Id"
                },
                new DataGridViewTextBoxColumn()
                {
                    Name = "descricao",
                    HeaderText = "Descrição"
                },
                new DataGridViewTextBoxColumn()
                {
                    Name = "valor",
                    HeaderText = "Valor"
                }
            };

            gridItens.Columns.AddRange(colunas);

            gridItens.ConfigurarGridSomenteLeitura();
            gridItens.ConfigurarGridZebrado();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Tema tema = ObterTema();

            string[] erros = tema.Validar();

            if (erros.Length > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }

        public void ConfigurarTela(Tema tema)
        {
            txtId.Text = tema.id.ToString();

            txtNome.Text = tema.nome;

            txtValor.Text = tema.CalcularValor().ToString();

            ConfigurarGridItens(tema.itens);
        }

        public void AtualizarRegistros(List<Item> itens)
        {
            gridItens.Rows.Clear();

            foreach (Item item in itens)
            {
                gridItens.Rows.Add(item.id, item.descricao, item.valor);
            }
        }

        private void btnSelecionarItens_Click(object sender, EventArgs e)
        {
            TelaSelecaoItensForm telaSelecaoItensForm = new TelaSelecaoItensForm(itensCadastrados, ObterTema());

            DialogResult resultado = telaSelecaoItensForm.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                ConfigurarGridItens(telaSelecaoItensForm.ObterItensMarcados());

                Tema temaCadastrado = ObterTema();

                txtValor.Text = temaCadastrado.CalcularValor().ToString();
            }

        }

        private void ConfigurarGridItens(List<Item> itensSelecionados)
        {
            gridItens.Rows.Clear();

            foreach (Item item in itensSelecionados)
            {
                gridItens.Rows.Add(item.id, item.descricao, item.valor);
            }
        }
    }
}
