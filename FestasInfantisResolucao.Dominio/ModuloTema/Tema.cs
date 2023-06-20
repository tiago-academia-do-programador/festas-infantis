using FestasInfantisResolucao.Dominio.Compartilhado;
using FestasInfantisResolucao.Dominio.ModuloItem;

namespace FestasInfantisResolucao.Dominio.ModuloTema
{
    public class Tema : EntidadeBase<Tema>
    {
        public string nome;
        public List<Item> itens;

        public Tema(string nome, List<Item> itens)
        {
            this.nome = nome;
            this.itens = itens;
        }

        public decimal CalcularValor()
        {
            return itens.Aggregate(0m, (soma, item) => soma + item.valor);
        }

        public override void AtualizarInformacoes(Tema registroAtualizado)
        {
            id = registroAtualizado.id;
            nome = registroAtualizado.nome;
            itens = registroAtualizado.itens;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome))
                erros.Add("O campo 'Nome' é obrigatório");

            if (nome.Length < 3)
                erros.Add("O campo 'Nome' deve conter no mínimo 3 caracteres");

            if (CalcularValor() <= 0)
                erros.Add("O campo 'Valor' não pode ser zerado.");

            return erros.ToArray();
        }
    }
}
