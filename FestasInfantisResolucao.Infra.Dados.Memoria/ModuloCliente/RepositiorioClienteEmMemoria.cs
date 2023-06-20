using FestasInfantisResolucao.Dominio.ModuloCliente;
using FestasInfantisResolucao.Infra.Dados.Memoria.Compartilhado;

namespace FestasInfantisResolucao.Infra.Dados.Memoria.ModuloCliente
{
    public class RepositorioClienteEmMemoria : RepositorioEmMemoriaBase<Cliente>, IRepositorioCliente
    {
        public RepositorioClienteEmMemoria(List<Cliente> clientes) : base(clientes)
        {
        }
    }
}
