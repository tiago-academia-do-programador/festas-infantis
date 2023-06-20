using FestasInfantisResolucao.Dominio.ModuloCliente;
using FestasInfantisResolucao.WinApp.Compartilhado;

namespace FestasInfantisResolucao.WinApp.ModuloCliente
{
    public class ControladorCliente : ControladorBase
    {
        private TabelaClienteControl tabelaCliente;
        private IRepositorioCliente repositorioCliente;

        public ControladorCliente(IRepositorioCliente repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        public override string ToolTipInserir { get { return "Inserir novo Cliente"; } }

        public override string ToolTipEditar { get { return "Editar Cliente existente"; } }

        public override string ToolTipExcluir { get { return "Excluir Cliente existente"; } }
        //public override string ToolTipVisualizarAlugueis { get { return "Visualizar Aluguéis do Cliente"; } }


        private void CarregarClientes()
        {
            List<Cliente> Clientes = repositorioCliente.SelecionarTodos();

            tabelaCliente.AtualizarRegistros(Clientes);
        }

        public override UserControl ObterListagem()
        {
            if (tabelaCliente == null)
                tabelaCliente = new TabelaClienteControl();

            CarregarClientes();

            return tabelaCliente;
        }

        public override void Inserir()
        {
            TelaClienteForm tela = new TelaClienteForm();

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Cliente clienteCadastrado = tela.ObterCliente();

                repositorioCliente.Inserir(clienteCadastrado);
            }

            CarregarClientes();
        }

        public override void Editar()
        {
            Cliente cliente = ObterClienteSelecionado();

            if (cliente == null)
            {
                MessageBox.Show($"Selecione um cliente primeiro!",
                    "Edição de Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            TelaClienteForm telaCliente = new TelaClienteForm();

            telaCliente.ConfigurarTela(cliente);

            DialogResult resultado = telaCliente.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Cliente clienteCadastrado = telaCliente.ObterCliente();

                repositorioCliente.Editar(clienteCadastrado.id, clienteCadastrado);
            }

            CarregarClientes();
        }

        public override void Excluir()
        {
            Cliente cliente = ObterClienteSelecionado();

            if (cliente == null)
            {
                MessageBox.Show($"Selecione um cliente primeiro!",
                    "Exclusão de Clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            DialogResult opcaoEscolhida = MessageBox.Show($"Deseja excluir o cliente {cliente.nome}?",
                "Exclusão de Clientes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcaoEscolhida == DialogResult.OK)
                repositorioCliente.Excluir(cliente);

            CarregarClientes();
        }

        public override string ObterTipoCadastro()
        {
            return "Cadastro de Clientes";
        }

        private Cliente ObterClienteSelecionado()
        {
            int id = tabelaCliente.ObterIdSelecionado();

            return repositorioCliente.SelecionarPorId(id);
        }

    }
}
