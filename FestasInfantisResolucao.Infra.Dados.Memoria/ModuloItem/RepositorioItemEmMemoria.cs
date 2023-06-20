using FestasInfantisResolucao.Dominio.ModuloItem;
using FestasInfantisResolucao.Infra.Dados.Memoria.Compartilhado;

namespace FestasInfantisResolucao.Infra.Dados.Memoria.ModuloItem
{
    public class RepositorioItemEmMemoria : RepositorioEmMemoriaBase<Item>, IRepositorioItem
    {
        public RepositorioItemEmMemoria(List<Item> itens) : base(itens)
        {
        }
    }
}
