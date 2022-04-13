using System;
using ControleBar.ConsoleApp.ModuloGarcom;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class RepositorioConta
    {
        private readonly List<Conta> contas;

        private int contadorId;

        public RepositorioConta()
        {
            contas = new List<Conta>();
        }

        public virtual string Inserir(Conta entidade)
        {
            entidade.id = ++contadorId;

            contas.Add(entidade);

            return "REGISTRO_VALIDO";
        }
        public string AdicionarPedido(Conta contaDesatualizada, Pedido pedido, Garcom garcom)
        {
            List<Pedido> pedidos = new List<Pedido>();
            pedidos.Add(pedido);
            Conta contaAtualizada = new Conta(contaDesatualizada.id, contaDesatualizada.nomeCliente, contaDesatualizada.DataCriacao, garcom, pedidos);

            contas.Remove(contaDesatualizada);
            contas.Add(contaAtualizada);
                
            return "REGISTRO_VALIDO";
        }

        public Conta SelecionarRegistro(int id)
        {
            return contas.Find(x => x.id == id);
        }
        public List<Conta> SelecionarTodos()
        {
            return contas;
        }
        public bool ExisteRegistro(int idSelecionado)
        {
            foreach (Conta conta in contas)
                if (conta.id == idSelecionado)
                    return true;

            return false;
        }

        public List<Conta> SelecionarTodasAbertas()
        {
            return contas.FindAll(x => x.Fechada == false);
        }
        public void Fechar(int idSelecionada)
        {
            foreach(Conta conta in contas)
            {
                if(conta.id == idSelecionada)
                    conta.Fechar();
                    
            }
        }
        public decimal TotalFaturado()
        {
            decimal valor = 0;
            List<Conta> contasFechadas = contas.FindAll(x => x.Fechada == true);
            foreach (Conta conta in contasFechadas)
                valor += conta.TotalConta;

            return valor;
        }
    }
}
