using System;
using System.Collections.Generic;
using System.Linq;
using ControleBar.ConsoleApp.Compartilhado;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase, ITelaCadastravel
    {

        private readonly IRepositorio<Mesa> _repositorioMesa;
        private readonly Notificador _notificador;
        public TelaMesa(IRepositorio<Mesa> repositorioMesa, Notificador notificador) 
            : base("Cadastro Mesa")
        {
            _repositorioMesa = repositorioMesa;
            _notificador = notificador;
        }
        public void Editar()
        {
            MostrarTitulo("Editando Garçom");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Mesa mesaAtualizada = ObterMesa();

            bool conseguiuEditar = _repositorioMesa.Editar(numeroGenero, mesaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Mesa");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMesa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMesa.Excluir(numeroMesa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Garçom");

            Mesa novaMesa = ObterMesa();

            _repositorioMesa.Inserir(novaMesa);

            _notificador.ApresentarMensagem("Mesa cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Mesas Cadastradas");

            List<Mesa> mesas = _repositorioMesa.SelecionarTodos();

            if (mesas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Mesa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Mesa mesa in mesas)
                Console.WriteLine(mesa.ToString());

            Console.ReadLine();

            return true;
        }

        #region métodos privados

        private Mesa ObterMesa()
        {
            Console.Write("Digite o numero da mesa: ");
            string identificador = Console.ReadLine();

            return new Mesa(identificador);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da mesa que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMesa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Mesa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        #endregion
    }
}
