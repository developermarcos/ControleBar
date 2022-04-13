using System;
using System.Collections.Generic;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class Pedido
    {
        private Mesa mesa;
        public List<Produto> produtos;
        string horaPedido;
        
        public Pedido()
        {
            produtos = new List<Produto>();
        }

        public Pedido(Mesa mesa, List<Produto> produtos)
        {
            this.mesa=mesa;
            this.produtos=produtos;
            this.horaPedido = new DateTime().Hour.ToString();
        }
    }
}
