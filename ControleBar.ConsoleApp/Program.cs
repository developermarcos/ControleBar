using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloConta;

namespace ControleBar.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelaMenuPrincipal telaMenuPrincipal = new TelaMenuPrincipal(new Notificador());

            while (true)
            {
                TelaBase telaSelecionada = telaMenuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    break;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastroBasico = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastroBasico.Inserir();

                    if (opcaoSelecionada == "2")
                        telaCadastroBasico.Editar();

                    if (opcaoSelecionada == "3")
                        telaCadastroBasico.Excluir();

                    if (opcaoSelecionada == "4")
                        telaCadastroBasico.VisualizarRegistros("Tela");

                }
                else
                {
                    if (telaSelecionada is TelaConta)
                    {
                        TelaConta telaConta = (TelaConta)telaSelecionada;

                        if (opcaoSelecionada == "1")
                            telaConta.Inserir();
                        if (opcaoSelecionada == "2")
                            telaConta.AdicionarPedido();
                        if (opcaoSelecionada == "3")
                            telaConta.Fechar();
                        if (opcaoSelecionada == "4")
                            telaConta.VisualizarRegistrosAbertos("Tela");
                        if (opcaoSelecionada == "5")
                            telaConta.TotalFaturado("Tela");
                        if (opcaoSelecionada == "6")
                            telaConta.VisualizarRegistros("Tela");
                        if (opcaoSelecionada == "7")
                            telaConta.TotalGarçon("Tela");
                    }
                }
            }
        }
    }
}
