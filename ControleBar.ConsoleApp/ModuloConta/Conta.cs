using System;
using System.Collections.Generic;
using System.Linq;
using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class Conta : EntidadeBase
    {
        public string nomeCliente;
        public List<Pedido> pedidos;
        DateTime dataCriacao;
        public Garcom garcom;
        private bool fechada;
        private decimal totalConta;

        public DateTime DataCriacao
        {
            get { return dataCriacao; }
        }
        public bool Fechada
        {
            get { return fechada;}
        }
        public decimal TotalConta
        {
            get { return totalConta; }
        }
        public Conta(string nomeCliente)
        {
            this.nomeCliente=nomeCliente;
            pedidos=new List<Pedido>();
            dataCriacao = DateTime.Now;
            this.fechada=false;
        }

        public Conta(int id, string nomeCliente, DateTime dataCriacao, Garcom garcom, List<Pedido> pedidos)
        {
            this.id=id;
            this.nomeCliente=nomeCliente;
            this.dataCriacao=dataCriacao;
            this.garcom=garcom;
            this.pedidos=pedidos;
            this.fechada=false;
        }

        public decimal ObterTotalConta()
        {
            decimal valorTotal = 0;
            foreach (var pedido in pedidos)
            {
                foreach(var produto in pedido.produtos)
                {
                    valorTotal += produto.Valor;
                }
            }
            return valorTotal;
        }
        public override string ToString()
        {
            string mensagem = $"ID: {id} | Cliente: {nomeCliente} | Total conta: {ObterTotalConta()}";
            return mensagem;
        }
        public void Fechar()
        {
            this.fechada = true;
            this.totalConta = ObterTotalConta();
        }
    }
}
