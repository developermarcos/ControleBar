using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleBar.ConsoleApp.Compartilhado;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase
    {
        private string identificador;

        public Mesa(string identificador)
        {
            this.identificador=identificador;
        }

        public override string ToString()
        {
            return $"Numero: {id} | Identificador: {identificador}";
        }
    }
}
