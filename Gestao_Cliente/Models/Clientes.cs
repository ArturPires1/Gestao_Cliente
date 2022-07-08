using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestao_Cliente.Models
{
    public class Clientes
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public string tipo_Cliente { get; set; }
        public string sexo { get; set; }
        public string situacao_Cliente { get; set; }
    }
}