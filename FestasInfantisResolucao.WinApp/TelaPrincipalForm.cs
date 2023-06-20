using FestasInfantisResolucao.Dominio.ModuloCliente;
using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.Dominio.ModuloTema;
using FestasInfantisResolucao.Infra.Dados.Memoria.ModuloCliente;
using FestasInfantisResolucao.Infra.Dados.Memoria.ModuloItem;
using FestasInfantisResolucao.Infra.Dados.Memoria.ModuloTema;
using FestasInfantisResolucao.WinApp.Compartilhado;
using FestasInfantisResolucao.WinApp.ModuloCliente;
using FestasInfantisResolucao.WinApp.ModuloItem;
using FestasInfantisResolucao.WinApp.ModuloTema;

namespace FestasInfantisResolucao.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;

        private IRepositorioCliente repositorioCliente =
            new RepositorioClienteEmMemoria(new List<Cliente>());

        private IRepositorioItem repositorioItem =
            new RepositorioItemEmMemoria(new List<Item>());

        private IRepositorioTema repositorioTema =
            new RepositorioTemaEmMemoria(new List<Tema>());

        private static TelaPrincipalForm telaPrincipal;

        public TelaPrincipalForm()
        {
            InitializeComponent();

            telaPrincipal = this;
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        public static TelaPrincipalForm Instancia
        {
            get
            {
                if (telaPrincipal == null)
                    telaPrincipal = new TelaPrincipalForm();

                return telaPrincipal;
            }
        }

        private void clientesMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorCliente(repositorioCliente);

            ConfigurarTelaPrincipal(controlador);
        }

        private void itensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorItem(repositorioItem);

            ConfigurarTelaPrincipal(controlador);
        }

        private void temasMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorTema(repositorioTema, repositorioItem);

            ConfigurarTelaPrincipal(controlador);
        }

        private void tarefasMenuItem_Click(object sender, EventArgs e)
        {
            //controlador = new ControladorTarefa(repositorioTarefa);

            ConfigurarTelaPrincipal(controlador);
        }

        private void categoriasMenuItem_Click(object sender, EventArgs e)
        {
            //controlador = new ControladorCategoria(repositorioCategoria);

            //ConfigurarTelaPrincipal(controlador);
        }

        private void despesasMenuItem_Click(object sender, EventArgs e)
        {
            //controlador = new ControladorDespesa(repositorioDespesa);

            //ConfigurarTelaPrincipal(controlador);
        }

        private void ConfigurarTelaPrincipal(ControladorBase controladorBase)
        {
            labelTipoCadastro.Text = controladorBase.ObterTipoCadastro();

            ConfigurarBarraFerramentas(controlador);

            ConfigurarListagem(controlador);
        }

        private void ConfigurarBarraFerramentas(ControladorBase controlador)
        {
            barraFerramentas.Enabled = true;

            ConfigurarToolTips(controlador);

            ConfigurarEstados(controlador);
        }

        private void ConfigurarListagem(ControladorBase controladorBase)
        {
            UserControl listagem = controladorBase.ObterListagem();

            listagem.Dock = DockStyle.Fill;

            panelRegistros.Controls.Clear();

            panelRegistros.Controls.Add(listagem);
        }

        private void ConfigurarToolTips(ControladorBase controlador)
        {
            btnInserir.ToolTipText = controlador.ToolTipInserir;
            btnEditar.ToolTipText = controlador.ToolTipEditar;
            btnExcluir.ToolTipText = controlador.ToolTipExcluir;
            btnFiltrar.ToolTipText = controlador.ToolTipFiltrar;
            //btnAdicionarItens.ToolTipText = controlador.ToolTipAdicionarItens;
            //btnConcluirItens.ToolTipText = controlador.ToolTipConcluirItens;
            //btnVisualizarAlugueis.ToolTipText = controlador.ToolTipVisualizarAlugueis;
        }

        private void ConfigurarEstados(ControladorBase controlador)
        {
            btnInserir.Enabled = controlador.InserirHabilitado;
            btnEditar.Enabled = controlador.EditarHabilitado;
            btnExcluir.Enabled = controlador.ExcluirHabilitado;
            btnFiltrar.Enabled = controlador.FiltrarHabilitado;
            //btnAdicionarItens.Enabled = controlador.AdicionarItensHabilitado;
            //btnConcluirItens.Enabled = controlador.ConcluirItensHabilitado;
            //btnVisualizarAlugueis.Enabled = controlador.VisualizarAlugueisHabilitado;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            controlador.Filtrar();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            controlador.Adicionar();
        }

        private void btnConcluirItens_Click(object sender, EventArgs e)
        {
            controlador.ConcluirItens();
        }

    }
}