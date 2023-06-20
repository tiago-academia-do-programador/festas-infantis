using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.WinApp.Compartilhado;

namespace FestasInfantisResolucao.WinApp.ModuloItem
{
    public class ControladorItem : ControladorBase
    {
        private IRepositorioItem repositorioItem;

        private TabelaItemControl tabelaItem;

        public override string ToolTipInserir { get { return "Inserir novo Item"; } }

        public override string ToolTipEditar { get { return "Editar Item existente"; } }

        public override string ToolTipExcluir { get { return "Excluir Item existente"; } }

        public ControladorItem(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem;
        }

        public override UserControl ObterListagem()
        {
            if (tabelaItem == null)
                tabelaItem = new TabelaItemControl();

            CarregarItems();

            return tabelaItem;
        }

        private void CarregarItems()
        {
            List<Item> Items = repositorioItem.SelecionarTodos();

            tabelaItem.AtualizarRegistros(Items);
        }

        public override string ObterTipoCadastro()
        {
            return "Cadastro de Itens";
        }

        public override void Inserir()
        {
            TelaItemForm telaItem = new TelaItemForm();

            DialogResult resultado = telaItem.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Item item = telaItem.ObterItem();

                repositorioItem.Inserir(item);
            }

            CarregarItems();
        }

        public override void Editar()
        {
            Item itemSelecionado = ObterItemSelecionado();

            if (itemSelecionado == null)
            {
                MessageBox.Show($"Selecione um item primeiro!",
                    "Edição de Itens",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            TelaItemForm telaItem = new TelaItemForm();

            telaItem.ConfigurarTela(itemSelecionado);

            DialogResult opcaoEscolhida = telaItem.ShowDialog();

            if (opcaoEscolhida == DialogResult.OK)
            {
                Item itemAtualizado = telaItem.ObterItem();

                repositorioItem.Editar(itemAtualizado.id, itemAtualizado);
            }

            CarregarItems();
        }

        public override void Excluir()
        {
            Item itemSelecionado = ObterItemSelecionado();

            if (itemSelecionado == null)
            {
                MessageBox.Show($"Selecione um Item primeiro!",
                    "Exclusão de Itens",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            DialogResult opcaoEscolhida = MessageBox.Show($"Deseja excluir o Item {itemSelecionado.descricao}?",
                "Exclusão de Itens",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcaoEscolhida == DialogResult.OK)
                repositorioItem.Excluir(itemSelecionado);

            CarregarItems();
        }

        private Item ObterItemSelecionado()
        {
            int id = tabelaItem.ObterIdSelecionado();

            return repositorioItem.SelecionarPorId(id);
        }
    }
}
