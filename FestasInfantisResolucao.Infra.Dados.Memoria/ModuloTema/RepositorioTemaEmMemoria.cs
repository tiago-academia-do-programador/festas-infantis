using FestasInfantisResolucao.Dominio.ModuloTema;
using FestasInfantisResolucao.Infra.Dados.Memoria.Compartilhado;

namespace FestasInfantisResolucao.Infra.Dados.Memoria.ModuloTema
{
    public class RepositorioTemaEmMemoria : RepositorioEmMemoriaBase<Tema>, IRepositorioTema
    {
        public RepositorioTemaEmMemoria(List<Tema> temas) : base(temas)
        {
        }
    }
}
