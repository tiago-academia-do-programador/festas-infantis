using FestasInfantisResolucao.Dominio.Compartilhado;

namespace FestasInfantisResolucao.Dominio.ModuloItem
{
    public class Item : EntidadeBase<Item>
    {
        public string descricao;
        public decimal valor;

        public Item(string descricao, decimal valor)
        {
            this.descricao = descricao;
            this.valor = valor;
        }

        public Item(int id, string descricao, decimal valor)
        {
            this.id = id;
            this.descricao = descricao;
            this.valor = valor;
        }

        public override void AtualizarInformacoes(Item registroAtualizado)
        {
            id = registroAtualizado.id;
            descricao = registroAtualizado.descricao;
            valor = registroAtualizado.valor;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(descricao))
                erros.Add("O campo 'Descrição' é obrigatório");

            if (descricao.Length < 3)
                erros.Add("O campo 'Descrição' deve conter no mínimo 3 caracteres");

            if (valor < 0)
                erros.Add("O item deve receber um valor maior que 0");

            return erros.ToArray();
        }

        public override string ToString()
        {
            return $"{descricao} R$ {valor}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Item item &&
                   id == item.id &&
                   descricao == item.descricao &&
                   valor == item.valor;
        }
    }
}
