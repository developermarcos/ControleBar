using System;
using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;
using ControleBar.ConsoleApp.ModuloGarcom;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase
    {
        private RepositorioConta _repositorioConta;
        private IRepositorio<Garcom> _repositorioGarcom;
        private IRepositorio<Mesa> _repositorioMesa;
        private IRepositorio<Produto> _repositorioProduto;
        private Notificador _notificador;
        private TelaCadastroGarcom _telaCadastroGarcom;
        private TelaMesa _telaMesa;
        private TelaProduto _telaProduto;

        public TelaConta(RepositorioConta repositorioConta, IRepositorio<Garcom> repositorioGarcom, IRepositorio<Mesa> repositorioMesa, IRepositorio<Produto> repositorioProduto, Notificador notificador, TelaMesa telaMesa, TelaProduto telaProduto, TelaCadastroGarcom telaGarcom)
            :base("Tela Conta")
        {
            this._repositorioConta=repositorioConta;
            this._repositorioGarcom=repositorioGarcom;
            this._repositorioMesa=repositorioMesa;
            this._repositorioProduto=repositorioProduto;
            this._notificador=notificador;
            this._telaCadastroGarcom = telaGarcom;
            this._telaMesa = telaMesa;
            this._telaProduto = telaProduto;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Adicionar pedido");
            Console.WriteLine("Digite 3 para Fechar");
            Console.WriteLine("Digite 4 para Visualizar Abertos");
            Console.WriteLine("Digite 5 para Visualizar Total Faturado");
            Console.WriteLine("Digite 6 para visualizar pedidos");
            Console.WriteLine("Digite 7 para Total Gorjetas");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Conta");

            Conta novaConta = ObterConta();
            
            _repositorioConta.Inserir(novaConta);

            _notificador.ApresentarMensagem("Conta cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void AdicionarPedido()
        {
            MostrarTitulo("Adicionando de Pedido");
            if (!VisualizarRegistros("Pesquisando"))
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada!", TipoMensagem.Atencao);
                return;
            }
            int idConta = ObterNumeroRegistro();
            Conta contaPedido = _repositorioConta.SelecionarRegistro(idConta);

            if (!_telaProduto.VisualizarRegistros("Pesquisando"))
            {
                _notificador.ApresentarMensagem("Nenhum produto cadastrado!", TipoMensagem.Atencao);
                return;
            }
            
            List<Produto> produtosPedido = ObterProdutos();

            foreach(Pedido pedido in contaPedido.pedidos)
                produtosPedido.AddRange(pedido.produtos);
            
            

            if (!_telaMesa.VisualizarRegistros("Pesquisando"))
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada!", TipoMensagem.Atencao);
                return;
            }
            Mesa mesaPedido = ObterMesa();

            if (!_telaCadastroGarcom.VisualizarRegistros("Pesquisando"))
            {
                _notificador.ApresentarMensagem("Nenhum garçom cadastrado!", TipoMensagem.Atencao);
                return;
            }
            Garcom garcomPedido = ObterGarcom();

            Pedido novoPedido = new Pedido(mesaPedido, produtosPedido);

            _repositorioConta.AdicionarPedido(contaPedido, novoPedido, garcomPedido);

            _notificador.ApresentarMensagem("Pedido adicionado com sucesso!", TipoMensagem.Sucesso);

        }

        public void Fechar()
        {
            MostrarTitulo("Fechar de Contas");

            List<Conta> Contas = _repositorioConta.SelecionarTodasAbertas();

            if (Contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada aberta.", TipoMensagem.Atencao);
                return;
            }

            int id = ObterNumeroRegistro();

            _repositorioConta.Fechar(id);

            Garcom garcom = _repositorioConta.SelecionarRegistro(id).garcom;

            garcom.Gorjeta = _repositorioConta.SelecionarRegistro(id).ObterTotalConta();

            _notificador.ApresentarMensagem("Conta fechada com sucesso!.", TipoMensagem.Sucesso);

        }

        public bool VisualizarRegistrosAbertos(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contas Abertas");

            List<Conta> Contas = _repositorioConta.SelecionarTodasAbertas();

            if (Contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada aberta.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta conta in Contas)
                Console.WriteLine(conta.ToString());

            Console.ReadKey();

            return true;
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contas Cadastradas");

            List<Conta> Contas = _repositorioConta.SelecionarTodos();

            if (Contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta conta in Contas)
                Console.WriteLine(conta.ToString());

            Console.ReadKey();

            return true;
        }

        public bool TotalFaturado(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização Total faturado");

            List<Conta> Contas = _repositorioConta.SelecionarTodos();

            if (Contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada.", TipoMensagem.Atencao);
                return false;
            }

            Console.WriteLine("Total faturado de R$ "+_repositorioConta.TotalFaturado());
            Console.ReadKey();

            return true;
        }

        public bool TotalGarçon(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Garçons Cadastrados");

            List<Garcom> garcons = _repositorioGarcom.SelecionarTodos();

            if (garcons.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum garçom disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Garcom garcom in garcons)
                Console.WriteLine("Garçom: "+garcom.Nome+" faturou: "+garcom.Gorjeta);

            Console.ReadLine();

            return true;
        }


        #region métodos privados

        private Conta ObterConta()
        {
            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            return new Conta(nomeCliente);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Mesa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        private int ObterNumeroRegistroParaFechar()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Mesa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        private Garcom ObterGarcom()
        {
            while (true)
            {
                Console.Write("Digite os ID do garcom: ");
                int id = Convert.ToInt32(Console.ReadLine());

                if (_repositorioGarcom.ExisteRegistro(id))
                {
                    Garcom garcom = _repositorioGarcom.SelecionarRegistro(id);

                    return garcom;
                }
                _notificador.ApresentarMensagem("ID informado não existe, tente novamente.", TipoMensagem.Atencao);
            }
        }

        private Mesa ObterMesa()
        {
            while (true)
            {
                Console.Write("Digite os ID da mesa: ");
                int id = Convert.ToInt32(Console.ReadLine());

                if (_repositorioMesa.ExisteRegistro(id))
                {
                    Mesa mesa = _repositorioMesa.SelecionarRegistro(id);

                    return mesa;
                }
                _notificador.ApresentarMensagem("ID informado não existe, tente novamente.", TipoMensagem.Atencao);
            }

        }

        private List<Produto> ObterProdutos()
        {
            Console.Write("Informe separado por ';' os IDs dos produto que deseja vincular ao pedido");
            Console.Write("Digite os IDs: ");
            string[] identificadores = Console.ReadLine().Split(";");
            List<Produto> produtosPedido = new List<Produto>();
            for (int i = 0; i < identificadores.Length; i++)
            {
                int id = Convert.ToInt32(identificadores[i]);
                if (_repositorioProduto.ExisteRegistro(id))
                {
                    Produto produto = _repositorioProduto.SelecionarRegistro(id);
                    produtosPedido.Add(produto);
                }
            }

            return produtosPedido;
        }

        #endregion
    }
}
