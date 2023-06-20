using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.Dominio.ModuloTema;

namespace FestasInfantisResolucao.WinApp.ModuloTema
{
    public partial class TelaSelecaoItensForm : Form
    {
        private List<Item> itensCadastrados;

        public TelaSelecaoItensForm(List<Item> itensCadastrados, Tema tema)
        {
            InitializeComponent();
            
            this.itensCadastrados = itensCadastrados;

            ConfigurarTela(tema);
        }

        public List<Item> ObterItensMarcados()
        {
            return listItensTema.CheckedItems.Cast<Item>().ToList();
        }

        private void ConfigurarTela(Tema tema)
        {
            listItensTema.Items.Clear();

            int i = 0;

            foreach (Item item in itensCadastrados)
            {
                listItensTema.Items.Add(item);

                if (tema.itens.Contains(item))
                    listItensTema.SetItemChecked(i, true);

                i++;
            }
        }
    }
}
