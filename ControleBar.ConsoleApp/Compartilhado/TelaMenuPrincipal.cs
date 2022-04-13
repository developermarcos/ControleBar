using ControleBar.ConsoleApp.ModuloConta;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;
using System;

namespace ControleBar.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        #region Declarações (Tela, Repositórios)
        //Garçon
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        //Mesa
        private readonly IRepositorio<Mesa> repositorioMesa;
        private readonly TelaMesa telaMesa;

        //Produto
        private readonly IRepositorio<Produto> repositorioProduto;
        private readonly TelaProduto telaProduto;

        // Conta
        private readonly RepositorioConta repositorioConta;
        private readonly TelaConta telaConta;

        #endregion

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioGarcom = new RepositorioGarcom();
            telaCadastroGarcom = new TelaCadastroGarcom(repositorioGarcom, notificador);

            repositorioMesa = new RepositorioMesa();
            telaMesa = new TelaMesa(repositorioMesa, notificador);

            repositorioProduto = new RepositorioProduto();
            telaProduto = new TelaProduto(repositorioProduto, notificador);

            repositorioConta = new RepositorioConta();
            telaConta = new TelaConta(repositorioConta, repositorioGarcom, repositorioMesa, repositorioProduto, notificador, telaMesa, telaProduto, telaCadastroGarcom);

            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Mesas de Bar 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Garçons");

            Console.WriteLine("Digite 2 para Gerenciar Mesas");

            Console.WriteLine("Digite 3 para Gerenciar Produtos");

            Console.WriteLine("Digite 4 para Gerenciar Contas");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroGarcom;

            else if (opcao == "2")
                tela = telaMesa;

            else if (opcao == "3")
                tela = telaProduto;

            else if (opcao == "4")
                tela = telaConta;

            else if (opcao == "5")
                tela = null;

            return tela;
        }

        private void PopularAplicacao()
        {
            var garcom = new Garcom("Julinho", "230.232.519-98");
            repositorioGarcom.Inserir(garcom);

            var mesa = new Mesa("17-b");
            repositorioMesa.Inserir(mesa);

            var produto1 = new Produto("agua", 4);
            var produto2 = new Produto("Coca", 6);
            repositorioProduto.Inserir(produto1);
            repositorioProduto.Inserir(produto2);
        }
    }
}
