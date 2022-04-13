using System;
using ControleBar.ConsoleApp.Compartilhado;

namespace ControleBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase
    {
        private string nome;
        private decimal valor;

        public decimal Valor
        {
            get { return valor; }
        }

        public Produto(string nome, decimal valor)
        {
            this.nome=nome;
            this.valor=valor;
        }

        public override string ToString()
        {
            return $"ID: {id} | Nome: {nome}";
        }
    }
}
