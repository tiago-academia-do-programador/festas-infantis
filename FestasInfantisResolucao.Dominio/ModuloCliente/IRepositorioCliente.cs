﻿namespace FestasInfantisResolucao.Dominio.ModuloCliente
{
    public interface IRepositorioCliente
    {
        void Inserir(Cliente novoCliente);
        void Editar(int id, Cliente cliente);
        void Excluir(Cliente clienteSelecionado);
        List<Cliente> SelecionarTodos();
        Cliente SelecionarPorId(int id);
    }
}
