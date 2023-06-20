using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.Dominio.ModuloTema;
using FestasInfantisResolucao.WinApp.Compartilhado;

namespace FestasInfantisResolucao.WinApp.ModuloTema
{
    public class ControladorTema : ControladorBase
    {
        private TabelaTemaControl tabelaTema;
        private IRepositorioTema repositorioTema;
        private IRepositorioItem repositorioItem;

        public ControladorTema(IRepositorioTema repositorioTema, IRepositorioItem repositorioItem)
        {
            this.repositorioTema = repositorioTema;
            this.repositorioItem = repositorioItem;
        }

        public override string ToolTipInserir { get { return "Inserir novo Tema"; } }

        public override string ToolTipEditar { get { return "Editar Tema existente"; } }

        public override string ToolTipExcluir { get { return "Excluir Tema existente"; } }

        public override void Inserir()
        {
            TelaTemaForm telaTema = new TelaTemaForm(repositorioItem.SelecionarTodos());

            DialogResult resultado = telaTema.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Tema tema = telaTema.ObterTema();

                repositorioTema.Inserir(tema);
            }

            CarregarTemas();
        }

        public override void Editar()
        {
            Tema tema = ObterTemaSelecionado();

            if (tema == null)
            {
                MessageBox.Show($"Selecione um tema primeiro!",
                    "Edição de Temas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            TelaTemaForm telaTema = new TelaTemaForm(repositorioItem.SelecionarTodos());

            telaTema.ConfigurarTela(tema);

            DialogResult resultado = telaTema.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Tema temaCadastrado = telaTema.ObterTema();

                repositorioTema.Editar(temaCadastrado.id, temaCadastrado);
            }

            CarregarTemas();
        }

        public override void Excluir()
        {
            Tema tema = ObterTemaSelecionado();

            if (tema == null)
            {
                MessageBox.Show($"Selecione um tema primeiro!",
                    "Exclusão de Temas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            DialogResult opcaoEscolhida = MessageBox.Show($"Deseja excluir o tema {tema.nome}?",
                "Exclusão de Temas",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcaoEscolhida == DialogResult.OK)
                repositorioTema.Excluir(tema);

            CarregarTemas();
        }

        public override UserControl ObterListagem()
        {
            if (tabelaTema == null)
                tabelaTema = new TabelaTemaControl();

            CarregarTemas();

            return tabelaTema;
        }

        public override string ObterTipoCadastro()
        {
            return "Cadastro de Temas";
        }

        private void CarregarTemas()
        {
            List<Tema> temas = repositorioTema.SelecionarTodos();

            tabelaTema.AtualizarRegistros(temas);
        }

        private Tema ObterTemaSelecionado()
        {
            int id = tabelaTema.ObterIdSelecionado();

            return repositorioTema.SelecionarPorId(id);
        }
    }
}
