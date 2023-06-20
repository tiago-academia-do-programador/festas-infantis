using FestasInfantisResolucao.Dominio.Compartilhado;

namespace FestasInfantisResolucao.Dominio.ModuloCliente
{
    public class Cliente : EntidadeBase<Cliente>
    {
        public string nome;
        public string telefone;

        public Cliente(string nome, string telefone)
        {
            this.nome = nome;
            this.telefone = telefone;
        }

        public override void AtualizarInformacoes(Cliente registroAtualizado)
        {
            id = registroAtualizado.id;
            nome = registroAtualizado.nome;
            telefone = registroAtualizado.telefone;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome))
                erros.Add("O campo 'Nome' é obrigatório");

            if (nome.Length <= 3)
                erros.Add("O campo 'Nome' deve conter no mínimo 3 caracteres");

            return erros.ToArray();
        }
    }
}